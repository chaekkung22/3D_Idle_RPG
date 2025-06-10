using UnityEngine;
using UnityEngine.AI;

public class PlayerChaseState : PlayerGroundState
{
    public PlayerChaseState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    private Vector3 targetPosition;
    private float chasingRange;
    private float targetDistanceSqr;

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.Agent.isStopped = false;
        chasingRange = stateMachine.Player.Data.DetectData.TargetChasingRange;
        SetAgentSpeed(stateMachine.Player.Data.GroundData.ChaseSpeedModifier);
        StartAnimation(stateMachine.Player.AnimationData.ChaseParameterHash);
        targetPosition = stateMachine.Target.transform.position; 
        targetDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
        stateMachine.Player.Agent.SetDestination(targetPosition);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        if (stateMachine.Target == null)
        {
            stateMachine.ChangeState(stateMachine.DetectState);
            Debug.Log("타겟 없음");
            return;
        }



        if (targetDistanceSqr < chasingRange * chasingRange)
        {
            stateMachine.ChangeState(stateMachine.DetectState);
            Debug.Log("타겟과 멀어져서 탐색하러 돌아감");
            return;
        }
    }
    
}