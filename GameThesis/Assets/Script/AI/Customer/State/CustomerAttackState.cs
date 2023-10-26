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

        customerStateManager.ApplyOutlineColor(customerStateManager.color_fighting, customerStateManager.f_outlineScale);

        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;

        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);


        customerStateManager.c_atkCollider.enabled = true;

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
