using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData 
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string detectParameterName = "Detect";
    [SerializeField] private string chaseParameterName = "Chase";
    
    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string comboAttackParameterName = "ComboAttack";
    
    public int GroundParameterHash {get; private set;}
    public int IdleParameterHash {get; private set;}
    public int ChaseParameterHash {get; private set;}
    public int DetectParameterHash {get; private set;}
    public int AttackParameterHash {get; private set;}
    public int ComboAttackParameterHash  {get; private set;}

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash =  Animator.StringToHash(idleParameterName);
        ChaseParameterHash =  Animator.StringToHash(chaseParameterName);
        DetectParameterHash =   Animator.StringToHash(detectParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(comboAttackParameterName);
    }
}
