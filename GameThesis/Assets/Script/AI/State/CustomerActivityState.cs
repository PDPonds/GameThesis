using UnityEngine;
using UnityEngine.AI;

public class CustomerActivityState : AIBaseState
{
    public override void EnterState(AIStateManager ai)
    {

    }

    public override void UpdateState(AIStateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        customerStateManager.anim.SetBool("fightState", false);

        customerStateManager.agent.velocity = Vector3.zero;

        customerStateManager.DisablePunch();

        if (!customerStateManager.b_canAtk)
        {
            customerStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (customerStateManager.f_currentAtkDelay <= 0)
            {
                customerStateManager.b_canAtk = true;
            }
        }

    }

}
