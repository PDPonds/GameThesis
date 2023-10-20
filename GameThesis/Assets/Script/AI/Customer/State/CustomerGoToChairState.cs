using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGoToChairState : BaseState
{
    int chairIndex;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        if (customerStateManager.c_tableObj != null)
        {
            chairIndex = Random.Range(0, customerStateManager.c_tableObj.g_chairs.Count);
        }
        customerStateManager.img_icon.enabled = false;
        customerStateManager.img_progressBar.enabled = false;
        customerStateManager.text_coin.SetActive(false);
        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.DisablePunch();

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("walk", true);
        customerStateManager.agent.speed = customerStateManager.f_walkSpeed;
        if (customerStateManager.c_tableObj != null)
        {
            ChairObj chair = customerStateManager.c_tableObj.g_chairs[chairIndex].GetComponent<ChairObj>();
            customerStateManager.agent.SetDestination(chair.t_sitPos.position);
            if (Vector3.Distance(customerStateManager.transform.position, chair.t_sitPos.position) <= 1f)
            {
                customerStateManager.anim.SetBool("walk", false);
                customerStateManager.anim.SetBool("sit", true);
                customerStateManager.anim.SetBool("drunk", false);
                customerStateManager.agent.velocity = Vector3.zero;
                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y - 0.3f, chair.t_sitPos.position.z);
                customerStateManager.transform.position = chairPos;
                customerStateManager.transform.rotation = Quaternion.Euler(0, -chair.transform.localEulerAngles.z, 0);
                customerStateManager.c_chairObj = chair;
                customerStateManager.SwitchState(customerStateManager.s_waitFoodState);
            }
        }



    }

}
