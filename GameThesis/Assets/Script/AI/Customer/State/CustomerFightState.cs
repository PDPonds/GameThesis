using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CustomerFightState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.img_icon.enabled = false;
        customerStateManager.img_progressBar.enabled = false;
        customerStateManager.text_coin.SetActive(false);
        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;

    }


    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        customerStateManager.agent.SetDestination(PlayerManager.Instance.transform.position);
        customerStateManager.agent.speed = customerStateManager.f_walkSpeed;
        customerStateManager.anim.SetBool("fightState", true);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);

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
            customerStateManager.agent.velocity = Vector2.zero;
            if (customerStateManager.b_canAtk)
            {
                customerStateManager.SwitchState(customerStateManager.s_attackState);
            }
        }

    }

}
