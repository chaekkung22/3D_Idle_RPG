using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    
    [field:Header("Animations")]
    [field:SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    
    public Animator Animator  { get; private set; }

    public Health health { get; private set; }
    private PlayerStateMachine stateMachine;
    
    private void Awake()
    {
        AnimationData.Initialize();
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        health.OnDie += OnDie;
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }
    

    void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }
}
