using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    private float searchCooldownTimer;
    
    public override void Enter()
    {
        stateMachine.Player.Agent.isStopped = true;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
        searchCooldownTimer = stateMachine.Player.Data.DetectData.SearchCooldownTime;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
            return;
        }
        
        searchCooldownTimer -= Time.deltaTime;

        if (searchCooldownTimer <= 0)
        {
            stateMachine.ChangeState(stateMachine.DetectState);
        }
    }
}
