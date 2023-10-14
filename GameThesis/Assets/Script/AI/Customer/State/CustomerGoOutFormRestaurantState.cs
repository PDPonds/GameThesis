using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGoOutFormRestaurantState : BaseState
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
        customerStateManager.img_icon.enabled = false;
        customerStateManager.img_progressBar.enabled = false;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("walk", true);

        customerStateManager.agent.SetDestination(GameManager.Instance.t_restaurantForntDoor.position);

        if (Vector3.Distance(customerStateManager.transform.position, GameManager.Instance.t_restaurantForntDoor.position)
            <= 0.5f)
        {
            customerStateManager.SwitchState(customerStateManager.s_walkAroundState);
        }

    }

}
