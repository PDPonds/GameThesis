using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaitFoodState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.f_currentOrderTime = customerStateManager.f_orderTime;
        customerStateManager.c_tableObj.s_currentCustomer = customerStateManager;

        Color noColor = new Color(0, 0, 0, 0);
        customerStateManager.ApplyOutlineColor(noColor, 0f);

        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;
        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        if (customerStateManager.c_tableObj != null)
        {
            if (customerStateManager.c_chairObj != null)
            {
                ChairObj chair = customerStateManager.c_chairObj;
                customerStateManager.anim.SetBool("walk", false);
                customerStateManager.anim.SetBool("run", false);
                customerStateManager.anim.SetBool("sit", true);
                customerStateManager.anim.SetBool("drunk", false);
                customerStateManager.agent.velocity = Vector3.zero;
                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y - 0.4f, chair.t_sitPos.position.z);
                customerStateManager.transform.position = chairPos;
                customerStateManager.transform.rotation = Quaternion.Euler(0, -chair.transform.localEulerAngles.z, 0);

            }

        }


        if (customerStateManager.f_currentOrderTime <= 0)
        {
            customerStateManager.SwitchState(customerStateManager.s_goOutState);
            RestaurantManager.Instance.RemoveRating();
        }

    }


}
