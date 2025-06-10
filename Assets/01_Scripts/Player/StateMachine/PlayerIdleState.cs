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
        base.Enter();
        stateMachine.Player.Agent.isStopped = true;
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

        if (detector.HasAnyTarget())
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
            Debug.Log("타겟 찾아서 쫓아가기 시작함");
            return;
        }
        
        searchCooldownTimer -= Time.deltaTime;

        if (searchCooldownTimer <= 0)
        {
            stateMachine.ChangeState(stateMachine.DetectState);
            Debug.Log("2초지나서 DetectState로 감");
            return;
        }
    }
}
