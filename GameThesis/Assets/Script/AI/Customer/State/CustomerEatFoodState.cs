using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEatFoodState : BaseState
{
    float f_eatTime;
    float f_currentEatTime;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        f_eatTime = Random.Range(cus.v_minAndMaxEatFood.x, cus.v_minAndMaxEatFood.y);
        f_currentEatTime = f_eatTime;

        Color noColor = new Color(0, 0, 0, 0);
        cus.ApplyOutlineColor(noColor,0f);

        cus.g_stunVFX.SetActive(false);
        cus.g_sleepVFX.SetActive(false);

        if (cus.c_chairObj != null)
        {
            cus.c_chairObj.EnableAllFood();
        }
       

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
                cus.anim.SetBool("drunk", false);
                cus.anim.SetBool("eat", true);

                cus.agent.velocity = Vector3.zero;
                Vector3 chairPos = new Vector3(chair.t_sitPos.position.x, chair.t_sitPos.position.y, chair.t_sitPos.position.z);
                cus.transform.position = chairPos;
                cus.transform.rotation = Quaternion.Euler(0, chair.transform.localEulerAngles.z + 90f, 0);
            }

        }

        f_currentEatTime -= Time.deltaTime;
        if (f_currentEatTime <= 0)
        {
            float drunkRan = Random.Range(0f, 100f);

            RestaurantManager.Instance.AddRating();

            if (drunkRan <= cus.f_drunkPercent)
            {
                cus.SwitchState(cus.s_drunkState);
            }
            else
            {
                float ran = Random.Range(0, 100f);

                if (ran <= cus.f_randomEventPercent)
                {
                    cus.c_chairObj.b_isEmpty = true;
                    cus.SwitchState(cus.s_escapeState);
                }
                else
                {
                    cus.c_chairObj.b_isEmpty = true;
                    cus.SwitchState(cus.s_goToCounterState);
                }
            }

        }

    }
}
