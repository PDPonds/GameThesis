using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerDrunkState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        cus.ApplyOutlineColor(cus.color_interact, cus.f_outlineScale);

        cus.b_isDrunk = true;

        cus.f_currentWekeUpPoint = 0;

        cus.g_sleepVFX.SetActive(true);
        cus.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        cus.RagdollOff();

        if (cus.c_chairObj != null)
        {
            if (cus.c_chairObj != null)
            {
                ChairObj chair = cus.c_chairObj;
                cus.anim.SetBool("walk", false);
                cus.anim.SetBool("sit", true);
                cus.anim.SetBool("drunk", true);
                cus.anim.SetBool("eat", false);
                cus.agent.velocity = Vector3.zero;
                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y, chair.t_sitPos.position.z);
                cus.transform.position = chairPos;
                cus.transform.rotation = Quaternion.Euler(0, chair.transform.localEulerAngles.z + 90f, 0);

            }
        }

        cus.f_currentWekeUpPoint -= 2 * Time.deltaTime;
        if (cus.f_currentWekeUpPoint < 0)
        {
            cus.f_currentWekeUpPoint = 0;
        }

    }
}
