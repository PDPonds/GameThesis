using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerDrunkState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.img_icon.enabled = false;

        customerStateManager.text_coin.SetActive(true);
        TextMeshProUGUI text = customerStateManager.text_coin.GetComponent<TextMeshProUGUI>();
        text.color = customerStateManager.color_escape;

        customerStateManager.img_progressBar.enabled = false;
        customerStateManager.b_isDrunk = true;
        customerStateManager.img_wakeUpImage.enabled = true;

        customerStateManager.img_BGWakeUpImage.enabled = true;

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        if (customerStateManager.c_tableObj != null)
        {
            if (customerStateManager.c_chairObj != null)
            {
                ChairObj chair = customerStateManager.c_chairObj;
                customerStateManager.anim.SetBool("walk", false);
                customerStateManager.anim.SetBool("sit", true);
                customerStateManager.anim.SetBool("drunk", true);
                customerStateManager.agent.velocity = Vector3.zero;
                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y - 0.3f, chair.t_sitPos.position.z);
                customerStateManager.transform.position = chairPos;
                customerStateManager.transform.rotation = Quaternion.Euler(0, -chair.transform.localEulerAngles.z, 0);

            }
        }

        customerStateManager.f_currentWekeUpPoint -= 2 * Time.deltaTime;
        if (customerStateManager.f_currentWekeUpPoint < 0)
        {
            customerStateManager.f_currentWekeUpPoint = 0;
        }

        float percent = customerStateManager.f_currentWekeUpPoint / customerStateManager.f_maxWekeUpPoint;
        customerStateManager.img_wakeUpImage.fillAmount = percent;

    }
}
