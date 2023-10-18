using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CustomerWalkAroundState : BaseState
{
    float f_currentTimeToWalk;
    float f_currentEscapeTime;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.i_currentHP = customerStateManager.i_maxHP;
        f_currentTimeToWalk = 0;
        if (customerStateManager.b_escape)
        {
            f_currentEscapeTime = customerStateManager.f_escapeTime;
        }
        customerStateManager.img_BGWakeUpImage.enabled = false;
        customerStateManager.img_wakeUpImage.enabled = false;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);
        customerStateManager.DisablePunch();

        customerStateManager.agent.speed = customerStateManager.f_walkSpeed;

        if (!customerStateManager.b_canAtk)
        {
            customerStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (customerStateManager.f_currentAtkDelay <= 0)
            {
                customerStateManager.b_canAtk = true;
            }
        }

        f_currentTimeToWalk -= Time.deltaTime;
        if (f_currentTimeToWalk <= 0)
        {
            float xPos = Random.Range(10f, 20f);
            float zPos = Random.Range(-13f, 13f);
            customerStateManager.v_walkPos = new Vector3(xPos, 0, zPos);
            f_currentTimeToWalk = customerStateManager.f_findNextPositionTime;
        }

        customerStateManager.agent.SetDestination(customerStateManager.v_walkPos);

        if (customerStateManager.agent.velocity != Vector3.zero)
        {
            customerStateManager.anim.SetBool("walk", true);
        }
        else
        {
            customerStateManager.anim.SetBool("walk", false);
        }

        if (customerStateManager.b_escape)
        {
            f_currentEscapeTime -= Time.deltaTime;
            if (f_currentEscapeTime <= 0)
            {
                customerStateManager.b_escape = false;
            }

            customerStateManager.img_icon.enabled = false;
            customerStateManager.img_progressBar.enabled = true;

            customerStateManager.text_coin.SetActive(true);
            TextMeshProUGUI text = customerStateManager.text_coin.GetComponent<TextMeshProUGUI>();
            text.color = customerStateManager.color_escape;

            float progressTime = f_currentEscapeTime / customerStateManager.f_escapeTime;

            customerStateManager.img_progressBar.fillAmount = progressTime;
            customerStateManager.img_progressBar.color = new Color(1 - progressTime, progressTime, 0, 1);

        }
        else
        {
            customerStateManager.img_icon.enabled = false;
            customerStateManager.img_progressBar.enabled = false;
            customerStateManager.text_coin.SetActive(false);

        }
    }


}
