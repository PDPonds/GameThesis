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

        //cus.f_giveCoin 
        cus.i_dish = RestaurantManager.Instance.menuHandler.RandomDish();
        cus.i_drink = RestaurantManager.Instance.menuHandler.RandomDrink();

        float f_dishCost = RestaurantManager.Instance.menuHandler.mainDish_Status[cus.i_dish].cost;
        float f_drinkCost = RestaurantManager.Instance.menuHandler.drinks_Status[cus.i_drink].cost;

        float cost = f_dishCost + f_drinkCost;

        float tips = Random.Range(cus.v_minmaxTipsCoin.x, cus.v_minmaxTipsCoin.y);

        cus.f_giveCoin = cost + tips;
        cus.PauseSleepSound();
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
                cus.anim.SetBool("eat", false);
                cus.anim.SetBool("checkbill", false);

                cus.agent.velocity = Vector3.zero;

                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y, chair.t_sitPos.position.z);
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
