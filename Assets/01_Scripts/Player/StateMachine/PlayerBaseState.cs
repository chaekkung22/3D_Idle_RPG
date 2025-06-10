using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected TargetDetector detector;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }
    
    public virtual void Enter()
    { 
        detector = new TargetDetector(stateMachine.Player.transform, stateMachine.Player.Data.DetectData.TargetChasingRange,
            LayerMask.GetMask("Enemy"));
        stateMachine.Player.Animator.applyRootMotion = false;
    }

    public virtual void Exit()
    {

    }
    

    public virtual void HandleInput()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }
    
    protected void SetAgentSpeed(float modifier)
    {
        stateMachine.Player.Agent.speed = stateMachine.Player.Data.GroundData.BaseSpeed * modifier;
    }

    // protected bool IsInChasingRange()
    // {
    //     if (stateMachine.Target.IsDie) return false;
    //     float targetDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
    //     return targetDistanceSqr <= stateMachine.Player.Data.DetectData.TargetChasingRange;
    // }
    
}
