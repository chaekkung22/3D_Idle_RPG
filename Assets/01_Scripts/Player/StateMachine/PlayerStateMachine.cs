using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }
    
    public PlayerIdleState IdleState { get; private set; }
    public PlayerDetectState DetectState { get; private set; }
    public PlayerChaseState ChaseState { get; private set; }
    public PlayerComboAttackState ComboAttackState  { get; private set; }
    
    public Health Target { get;  set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;
        IdleState = new PlayerIdleState(this);
        DetectState = new PlayerDetectState(this);
        ChaseState = new PlayerChaseState(this);
        ComboAttackState = new PlayerComboAttackState(this);
    }
}
