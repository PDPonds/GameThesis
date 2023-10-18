using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerFrontOfCounterState : BaseState
{
    float f_currentPayTime;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.img_icon.enabled = false;

        customerStateManager.text_coin.SetActive(true);
        TextMeshProUGUI text = customerStateManager.text_coin.GetComponent<TextMeshProUGUI>();
        text.color = customerStateManager.color_pay;

        customerStateManager.img_progressBar.enabled = true;
        f_currentPayTime = customerStateManager.f_payTime;

        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.agent.velocity = Vector3.zero;
        customerStateManager.anim.SetBool("walk", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);
        customerStateManager.anim.SetBool("fightState", false);

        f_currentPayTime -= Time.deltaTime;
        if (f_currentPayTime < 0)
        {
            customerStateManager.SwitchState(customerStateManager.s_goOutState);
        }

        float progressTime = f_currentPayTime / customerStateManager.f_payTime;

        customerStateManager.img_progressBar.color = new Color(1 - progressTime, progressTime, 0, 1);

        customerStateManager.img_progressBar.fillAmount = progressTime;

    }


}
