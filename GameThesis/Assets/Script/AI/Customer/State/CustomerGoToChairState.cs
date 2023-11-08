using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGoToChairState : BaseState
{
    int chairIndex;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        Color noColor = new Color(0, 0, 0, 0);
        cus.ApplyOutlineColor(noColor, 0f);

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        cus.anim.SetBool("fightState", false);
        cus.anim.SetBool("walk", true);
        cus.agent.speed = cus.f_walkSpeed;
        if (cus.c_chairObj != null)
        {
            ChairObj chair = cus.c_chairObj;
            cus.agent.SetDestination(chair.t_sitPos.position);
            if (Vector3.Distance(cus.transform.position, chair.t_sitPos.position) <= 1f)
            {
                cus.anim.SetBool("walk", false);
                cus.anim.SetBool("sit", true);
                cus.anim.SetBool("drunk", false);
                cus.agent.velocity = Vector3.zero;
                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y - 0.3f, chair.t_sitPos.position.z);
                cus.transform.position = chairPos;
                cus.transform.rotation = Quaternion.Euler(0, -chair.transform.localEulerAngles.z, 0);
                cus.c_chairObj = chair;
                cus.SwitchState(cus.s_waitFoodState);
            }
        }



    }

}
