using UnityEngine;
using UnityEngine.AI;

public class CustomerAttackState : AIBaseState
{
    public override void EnterState(AIStateManager ai)
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
    }

    public override void UpdateState(AIStateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.agent.velocity = Vector3.zero;

        if (customerStateManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            customerStateManager.SwitchState(customerStateManager.s_fightState);
        }

    }
}
