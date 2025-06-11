using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    private bool alreadyAppliedCombo;
    private bool alreadyAppliedDealing;
    
    private float targetDistanceSqr;

    private AttackInfoData attackInfoData;

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.Agent.isStopped = true;
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);
        
        alreadyAppliedCombo = false;
        alreadyAppliedDealing  = false;
        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfoData(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo",  comboIndex);
        
        stateMachine.Player.Weapon.SetAttack(attackInfoData.Damage);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);
        if (!alreadyAppliedCombo)
        {
            stateMachine.ComboIndex = 0;
        }
        
        stateMachine.Player.Weapon.gameObject.SetActive(false);
    }
    
    public override void Update()
    {
        base.Update();

        float normalizeTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizeTime < 1f)
        {
            if (normalizeTime >= attackInfoData.ComboTransitionTime)
            {
                //콤보 시도
                TryComboAttack();
            }
            
            if (!alreadyAppliedDealing && normalizeTime >= attackInfoData.Dealing_Start_TransitionTime)
            {
                EnableWeapon();
            }

            if (alreadyAppliedDealing && normalizeTime >= attackInfoData.Dealing_End_TransitionTime)
            {
                DisableWeapon();
            }
        }
        else
        {
            if (alreadyAppliedCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
    
    private void EnableWeapon()
    {
        alreadyAppliedDealing = true;
        stateMachine.Player.Weapon.gameObject.SetActive(true);
        
    }
    
    
    private void DisableWeapon()
    {
        if (!alreadyAppliedDealing) return;
        stateMachine.Player.Weapon.gameObject.SetActive(false);
    }

    void TryComboAttack()
    {
        if (alreadyAppliedCombo) return;
        if (attackInfoData.ComboStateIndex == -1) return;
        if(!stateMachine.IsAttacking) return;
        
        alreadyAppliedCombo = true;
    }
    
    
    float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        //전환되고 욌을 때 && 다음 애니메이션이 tag
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))  //전환되고 있지 않을 때 && 현재 애니메이션이 tag
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}