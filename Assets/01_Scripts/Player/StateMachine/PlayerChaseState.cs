using UnityEngine;

public class PlayerChaseState : PlayerGroundState
{
    public PlayerChaseState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.Agent.isStopped = false;
        SetAgentSpeed(stateMachine.Player.Data.GroundData.ChaseSpeedModifier);
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ChaseParameterHash);
    }
}