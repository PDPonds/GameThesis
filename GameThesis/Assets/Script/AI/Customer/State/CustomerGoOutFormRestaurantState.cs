using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGoOutFormRestaurantState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        if (cus.c_chairObj != null)
        {
            cus.c_chairObj.DisableAllFood();
            cus.c_chairObj.b_isEmpty = true;
            cus.c_chairObj.b_readyForNextCustomer = false;
            cus.c_chairObj.s_currentCustomer = null;
        }
        cus.c_chairObj = null;

        Color noColor = new Color(0, 0, 0, 0);
        cus.ApplyOutlineColor(noColor, 0f);

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        cus.anim.SetBool("fightState", false);
        cus.anim.SetBool("sit", false);
        cus.anim.SetBool("drunk", false);
        cus.anim.SetBool("walk", true);
        cus.agent.speed = cus.f_walkSpeed;
        cus.agent.SetDestination(GameManager.Instance.t_restaurantForntDoor.position);

        if (Vector3.Distance(cus.transform.position, GameManager.Instance.t_restaurantForntDoor.position)
            <= 0.5f)
        {
            cus.SwitchState(cus.s_walkAroundState);
        }

    }

}
