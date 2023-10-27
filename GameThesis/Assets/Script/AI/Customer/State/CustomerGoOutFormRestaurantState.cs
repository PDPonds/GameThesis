using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGoOutFormRestaurantState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        if (customerStateManager.c_chairObj != null)
        {
            customerStateManager.c_chairObj.b_isEmpty = true;
            customerStateManager.c_chairObj.b_readyForNextCustomer = false;
            customerStateManager.c_chairObj.s_currentCustomer = null;
        }
        customerStateManager.c_chairObj = null;

        Color noColor = new Color(0, 0, 0, 0);
        customerStateManager.ApplyOutlineColor(noColor, 0f);

        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);
        customerStateManager.anim.SetBool("walk", true);
        customerStateManager.agent.speed = customerStateManager.f_walkSpeed;
        customerStateManager.agent.SetDestination(GameManager.Instance.t_restaurantForntDoor.position);

        if (Vector3.Distance(customerStateManager.transform.position, GameManager.Instance.t_restaurantForntDoor.position)
            <= 0.5f)
        {
            customerStateManager.SwitchState(customerStateManager.s_walkAroundState);
        }

    }

}
