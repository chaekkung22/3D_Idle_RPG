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
    private float attackRange;

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.Agent.isStopped = false;
        chasingRange = stateMachine.Player.Data.DetectData.TargetChasingRange;
        SetAgentSpeed(stateMachine.Player.Data.GroundData.ChaseSpeedModifier);
        StartAnimation(stateMachine.Player.AnimationData.ChaseParameterHash);
        attackRange = stateMachine.Player.Data.AttackData.AttackInfoDatas[0].AttackRange;
        var target = detector.DetectClosestTarget();
        
        if (target == null)
        {
            Debug.LogWarning("탐색 중 타겟을 못 찾았어요... → Idle 상태로 전환");
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        stateMachine.Target = target;
        targetPosition = stateMachine.Target.transform.position;

        stateMachine.Player.Agent.SetDestination(targetPosition);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        targetDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
        
        if (stateMachine.Target == null)
        {
            stateMachine.ChangeState(stateMachine.DetectState);
            Debug.Log("타겟 없음");
            return;
        }

        if ( attackRange * attackRange > targetDistanceSqr )
        {
            stateMachine.ChangeState(stateMachine.ComboAttackState);
        }

        if ( chasingRange * chasingRange < targetDistanceSqr )
        {
            stateMachine.ChangeState(stateMachine.DetectState);
            Debug.Log("타겟과 멀어져서 탐색하러 돌아감");
            return;
        }
    }
    
}