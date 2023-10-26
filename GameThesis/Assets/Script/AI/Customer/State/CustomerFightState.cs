using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CustomerFightState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.ApplyOutlineColor(customerStateManager.color_fighting, customerStateManager.f_outlineScale);

        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;
        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        customerStateManager.DisablePunch();

        if (!customerStateManager.b_canAtk)
        {
            customerStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (customerStateManager.f_currentAtkDelay <= 0)
            {
                customerStateManager.b_canAtk = true;
            }
        }


        customerStateManager.agent.SetDestination(PlayerManager.Instance.transform.position);

        Collider[] player = Physics.OverlapSphere(ai.transform.position, customerStateManager.f_runRange, GameManager.Instance.lm_playerMask);
        if (player.Length > 0)
        {
            customerStateManager.agent.speed = customerStateManager.f_walkSpeed;

            customerStateManager.anim.SetBool("fightState", true);
            customerStateManager.anim.SetBool("walk", false);
            customerStateManager.anim.SetBool("run", false);
            customerStateManager.anim.SetBool("sit", false);
            customerStateManager.anim.SetBool("drunk", false);

            if (Vector3.Distance(PlayerManager.Instance.transform.position, customerStateManager.transform.position) <=
                customerStateManager.f_atkRange)
            {
                customerStateManager.agent.velocity = Vector2.zero;
                if (customerStateManager.b_canAtk)
                {
                    customerStateManager.SwitchState(customerStateManager.s_attackState);
                }
            }

        }
        else
        {
            customerStateManager.agent.speed = customerStateManager.f_runSpeed;
            customerStateManager.anim.SetBool("fightState", false);
            customerStateManager.anim.SetBool("walk", false);
            customerStateManager.anim.SetBool("run", true);
            customerStateManager.anim.SetBool("sit", false);
            customerStateManager.anim.SetBool("drunk", false);
        }


    }

}
