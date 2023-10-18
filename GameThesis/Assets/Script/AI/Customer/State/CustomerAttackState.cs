using UnityEngine;
using UnityEngine.AI;

public class CustomerAttackState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.i_atkCount++;
        customerStateManager.b_canAtk = false;
        customerStateManager.f_currentAtkDelay = customerStateManager.f_atkDelay;

        if (customerStateManager.i_atkCount % 2 == 0)
        {
            customerStateManager.anim.Play("LeftPunch");
        }
        else
        {
            customerStateManager.anim.Play("RightPunch");
        }
        customerStateManager.img_icon.enabled = false;
        customerStateManager.text_coin.SetActive(false);
        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.agent.velocity = Vector3.zero;

        if (customerStateManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            customerStateManager.SwitchState(customerStateManager.s_fightState);
        }

    }
}
