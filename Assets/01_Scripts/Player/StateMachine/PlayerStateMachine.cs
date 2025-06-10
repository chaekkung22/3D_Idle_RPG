using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public float MovementSpeed { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }
    
    public Health Target { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;
        Target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();

        MovementSpeed = player.Data.GroundData.BaseSpeed;
    }
}
