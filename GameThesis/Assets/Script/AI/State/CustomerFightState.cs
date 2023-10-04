using UnityEngine;
using UnityEngine.AI;

public class CustomerFightState : AIBaseState
{
    public override void EnterState(AIStateManager ai)
    {

    }

    public override void UpdateState(AIStateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        customerStateManager.agent.SetDestination(PlayerManager.Instance.transform.position);

        customerStateManager.anim.SetBool("fightState", true);

        customerStateManager.DisablePunch();

        if (!customerStateManager.b_canAtk)
        {
            customerStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (customerStateManager.f_currentAtkDelay <= 0)
            {
                customerStateManager.b_canAtk = true;
            }
        }

        Collider[] player = Physics.OverlapSphere(ai.transform.position, customerStateManager.f_atkRange, GameManager.Instance.lm_playerMask);
        if (player.Length > 0)
        {
            if (customerStateManager.b_canAtk)
            {
                customerStateManager.SwitchState(customerStateManager.s_attackState);
            }
        }

    }

}
