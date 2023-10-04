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

        Debug.Log("Customer Atk");
    }

    public override void UpdateState(AIStateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.RagdollOff();
        customerStateManager.agent.velocity = Vector3.zero;

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

        if (customerStateManager.anim.GetCurrentAnimatorStateInfo(0).length > 0.9f)
        {
            customerStateManager.SwitchState(customerStateManager.s_fightState);
        }

    }
}
