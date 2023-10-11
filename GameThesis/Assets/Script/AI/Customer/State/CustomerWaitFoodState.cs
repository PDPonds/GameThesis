using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaitFoodState : BaseState
{
    float f_currentOrderTime;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        f_currentOrderTime = customerStateManager.f_orderTime;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        if(customerStateManager.c_tableObj != null)
        {
            if(customerStateManager.c_chairObj != null)
            {
                ChairObj chair = customerStateManager.c_chairObj;
                customerStateManager.anim.SetBool("walk", false);
                customerStateManager.anim.SetBool("sit", true);
                customerStateManager.agent.velocity = Vector3.zero;
                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y - 0.3f, chair.t_sitPos.position.z);
                customerStateManager.transform.position = chairPos;
                customerStateManager.transform.rotation = Quaternion.Euler(0, -chair.transform.localEulerAngles.z, 0);

            }

        }

        f_currentOrderTime -= Time.deltaTime;
        if(f_currentOrderTime <= 0)
        {
            customerStateManager.SwitchState(customerStateManager.s_goOutState);
        }

    }

    
}
