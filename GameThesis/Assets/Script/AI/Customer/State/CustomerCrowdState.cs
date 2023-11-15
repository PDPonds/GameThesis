using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCrowdState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        Color noColor = new Color(0, 0, 0, 0);
        cus.ApplyOutlineColor(noColor, 0f);
        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

        int CheerAnimCount = Random.Range(0, cus.allCheerAnim.Count);
        cus.anim.runtimeAnimatorController = cus.allCheerAnim[CheerAnimCount];
        cus.PauseSleepSound();
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        cus.agent.speed = cus.f_runSpeed;
        cus.agent.SetDestination(cus.v_crowdPos);

        cus.anim.SetBool("fightState", false);
        cus.anim.SetBool("walk", false);
        cus.anim.SetBool("run", true);
        cus.anim.SetBool("sit", false);
        cus.anim.SetBool("drunk", false);
        cus.anim.SetBool("cheer", false);
        cus.anim.SetBool("eat", false);
        cus.anim.SetBool("checkbill", false);


        if (Vector3.Distance(cus.transform.position,
            cus.v_crowdPos) <= 1f)
        {
            cus.anim.SetBool("fightState", false);
            cus.anim.SetBool("walk", false);
            cus.anim.SetBool("run", false);
            cus.anim.SetBool("sit", false);
            cus.anim.SetBool("drunk", false);
            cus.anim.SetBool("cheer", true);

            cus.agent.velocity = Vector3.zero;
            cus.transform.LookAt(PlayerManager.Instance.transform.position);
        }

        if (Vector3.Distance(cus.transform.position, GameManager.Instance.t_crowdCenterPoint.position)
            > GameManager.Instance.f_crowdArea)
        {
            cus.SwitchState(cus.s_walkAroundState);
        }

    }
}
