using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGoToChairState : BaseState
{
    int chairIndex;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        Color noColor = new Color(0, 0, 0, 0);
        customerStateManager.ApplyOutlineColor(noColor, 0f);

        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);


    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.DisablePunch();

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("walk", true);
        customerStateManager.agent.speed = customerStateManager.f_walkSpeed;
        if (customerStateManager.c_chairObj != null)
        {
            ChairObj chair = customerStateManager.c_chairObj;
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
