using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerDrunkState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.ApplyOutlineColor(customerStateManager.color_interact, customerStateManager.f_outlineScale);

        customerStateManager.b_isDrunk = true;

        customerStateManager.f_currentWekeUpPoint = 0;

        customerStateManager.g_sleepVFX.SetActive(true);
        customerStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        if (customerStateManager.c_chairObj != null)
        {
            if (customerStateManager.c_chairObj != null)
            {
                ChairObj chair = customerStateManager.c_chairObj;
                customerStateManager.anim.SetBool("walk", false);
                customerStateManager.anim.SetBool("sit", true);
                customerStateManager.anim.SetBool("drunk", true);
                customerStateManager.agent.velocity = Vector3.zero;
                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y - 0.4f, chair.t_sitPos.position.z);
                customerStateManager.transform.position = chairPos;
                customerStateManager.transform.rotation = Quaternion.Euler(0, chair.transform.localEulerAngles.z + 90f, 0);

            }
        }

        customerStateManager.f_currentWekeUpPoint -= 2 * Time.deltaTime;
        if (customerStateManager.f_currentWekeUpPoint < 0)
        {
            customerStateManager.f_currentWekeUpPoint = 0;
        }

    }
}
