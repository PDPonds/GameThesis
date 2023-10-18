using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerGoToCounterState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        if (customerStateManager.c_tableObj != null)
        {
            customerStateManager.c_tableObj.b_isEmtry = true;
            customerStateManager.c_tableObj.b_readyForNextCustomer = false;
            customerStateManager.c_tableObj.s_currentCustomer = null;
        }
        customerStateManager.c_tableObj = null;
        customerStateManager.c_chairObj = null;

        customerStateManager.img_progressBar.enabled = false;
        customerStateManager.img_icon.enabled = false;
        customerStateManager.text_coin.SetActive(true);
        TextMeshProUGUI text = customerStateManager.text_coin.GetComponent<TextMeshProUGUI>();
        text.color = customerStateManager.color_pay;
        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);
        customerStateManager.anim.SetBool("walk", true);
        customerStateManager.agent.SetDestination(GameManager.Instance.t_counterPos.position);
        customerStateManager.agent.speed = customerStateManager.f_walkSpeed;
        if (Vector3.Distance(customerStateManager.transform.position, GameManager.Instance.t_counterPos.position)
            <= 1f)
        {
            customerStateManager.SwitchState(customerStateManager.s_frontCounter);
        }
    }

}
