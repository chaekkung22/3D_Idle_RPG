using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }
    
    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
    

    public void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    protected bool IsInChasingRange()
    {
        if (stateMachine.Target.IsDie) return false;
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Player.Data.TargetChasingRange;
    }
    
}
