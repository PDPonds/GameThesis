using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaitFoodState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        cus.f_currentOrderTime = cus.f_orderTime;
        cus.c_chairObj.s_currentCustomer = cus;

        if (cus.c_chairObj != null)
        {
            cus.c_chairObj.DisableAllFood();
        }

        Color noColor = new Color(0, 0, 0, 0);
        cus.ApplyOutlineColor(noColor, 0f);

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        if (cus.c_chairObj != null)
        {
            if (cus.c_chairObj != null)
            {
                ChairObj chair = cus.c_chairObj;
                cus.anim.SetBool("walk", false);
                cus.anim.SetBool("run", false);
                cus.anim.SetBool("sit", true);
                cus.anim.SetBool("drunk", false);
                cus.agent.velocity = Vector3.zero;

                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y - 0.4f, chair.t_sitPos.position.z);
                cus.transform.position = chairPos;
                cus.transform.rotation = Quaternion.Euler(0, chair.transform.localEulerAngles.z + 90f, 0);

            }

        }

        cus.f_currentOrderTime -= Time.deltaTime;
        if (cus.f_currentOrderTime <= 0)
        {
            cus.SwitchState(cus.s_goOutState);
            RestaurantManager.Instance.RemoveRating();
        }

    }


}
