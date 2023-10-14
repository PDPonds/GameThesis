using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEatFoodState : BaseState
{
    float f_eatTime;
    float f_currentEatTime;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        f_eatTime = Random.Range(customerStateManager.v_minAndMaxEatFood.x, customerStateManager.v_minAndMaxEatFood.y);
        f_currentEatTime = f_eatTime;
        customerStateManager.img_icon.enabled = false;
        customerStateManager.img_progressBar.enabled = false;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        if (customerStateManager.c_tableObj != null)
        {
            if (customerStateManager.c_chairObj != null)
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

        f_currentEatTime -= Time.deltaTime;
        if (f_currentEatTime <= 0)
        {
            float ran = Random.Range(0, 100f);

            if (ran <= customerStateManager.f_randomEventPercent)
            {
                customerStateManager.SwitchState(customerStateManager.s_escapeState);
            }
            else
            {
                customerStateManager.SwitchState(customerStateManager.s_goToCounterState);
            }
        }

    }
}
