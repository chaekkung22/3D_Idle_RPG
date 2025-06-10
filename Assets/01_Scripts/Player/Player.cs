using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }
    
    [field:Header("Animations")]
    [field:SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    
    public Animator Animator  { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public Health health { get; private set; }
    private PlayerStateMachine stateMachine;
    
    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        health = GetComponent<Health>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        health.OnDie += OnDie;
    }

    void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }
}
