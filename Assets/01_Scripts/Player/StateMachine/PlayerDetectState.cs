using UnityEngine;
using UnityEngine.AI;

public class PlayerDetectState : PlayerGroundState
{
    public PlayerDetectState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    private float timer = 30f;
    private float refreshTimer = 1f;
    private Transform player;

    public override void Enter()
    {
 
        base.Enter();
        timer = 30f;
        stateMachine.Player.Agent.isStopped = false;
        player = stateMachine.Player.transform;
        SetAgentSpeed(stateMachine.Player.Data.GroundData.DetectSpeedModifier);
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
        timer -= Time.deltaTime;

        if (detector.HasAnyTarget())
        {
            Debug.Log("타겟찾음");
            stateMachine.Target = detector.DetectClosestTarget();
            stateMachine.ChangeState(stateMachine.ChaseState);
            return;
        }
        
        
        if (HasReachedDestination())
        {
            Debug.Log("목적지도착");
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        else if (timer <= 0f)
        {
            Debug.Log("시간종료");
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private Vector3 GetDetectLocation()
    {
        float maxDistance = stateMachine.Player.Data.DetectData.SearchDistance;
        NavMeshHit hit;
        NavMesh.SamplePosition(player.position + player.forward * maxDistance, out hit, maxDistance, 
            NavMesh.AllAreas);
        return hit.position;
    }
    
    private bool HasReachedDestination()
    {
        var agent = stateMachine.Player.Agent;

        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance;
    }
}