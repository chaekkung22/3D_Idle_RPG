using UnityEngine;
using UnityEngine.AI;

public class PlayerDetectState : PlayerGroundState
{
    public PlayerDetectState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        stateMachine.Player.Agent.isStopped = false;
        SetAgentSpeed(stateMachine.Player.Data.GroundData.DetectSpeedModifier);
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.DetectParameterHash);
        stateMachine.Player.Agent.SetDestination(GetDetectLocation());
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.DetectParameterHash);
    }

    public override void Update()
    {
        base.Update();
        
        TargetDetector detector = new TargetDetector(stateMachine.Player.transform, stateMachine.Player.Data.DetectData.TargetChasingRange,
            LayerMask.GetMask("Enemy"));

        var closestTarget = detector.DetectClosestTarget();

        if (closestTarget != null)
        {
            stateMachine.Target = closestTarget;
            stateMachine.ChangeState(stateMachine.ChaseState);
            return;
        }

        if (HasReachedDestination())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private Vector3 GetDetectLocation()
    {
        float maxDistance = stateMachine.Player.Data.DetectData.SearchDistance;
        NavMeshHit hit;
        NavMesh.SamplePosition(stateMachine.Player.transform.forward * maxDistance, out hit, maxDistance, 
            NavMesh.AllAreas);
        return hit.position;
    }
    
    private bool HasReachedDestination()
    {
        var agent = stateMachine.Player.Agent;

        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance &&
               !agent.hasPath;
    }
}