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
            customerStateManager.c_leftHandPunch.enabled = true;
        }
        else
        {
            customerStateManager.anim.Play("RightPunch");
            customerStateManager.c_rightHandPunch.enabled = true;

        }
        customerStateManager.img_icon.enabled = false;
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
