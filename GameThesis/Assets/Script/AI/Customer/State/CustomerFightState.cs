using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CustomerFightState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.ApplyOutlineColor(customerStateManager.color_fighting, customerStateManager.f_outlineScale);

        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        //customerStateManager.agent.speed = customerStateManager.f_walkSpeed;

        customerStateManager.anim.SetBool("fightState", true);
        customerStateManager.anim.SetBool("walk", false);
        customerStateManager.anim.SetBool("run", false);
        customerStateManager.anim.SetBool("sit", false);
    }

}
