using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerThrongState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.agent.speed = customerStateManager.f_runSpeed;
        customerStateManager.agent.SetDestination(GameManager.Instance.t_thongCenterPoint.position);

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("walk", false);
        customerStateManager.anim.SetBool("run", true);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);
        customerStateManager.anim.SetBool("cheer", false);

        if (Vector3.Distance(customerStateManager.transform.position,
            GameManager.Instance.t_thongCenterPoint.position) <= GameManager.Instance.f_throngDistance)
        {
            customerStateManager.anim.SetBool("fightState", false);
            customerStateManager.anim.SetBool("walk", false);
            customerStateManager.anim.SetBool("run", false);
            customerStateManager.anim.SetBool("sit", false);
            customerStateManager.anim.SetBool("drunk", false);
            customerStateManager.anim.SetBool("cheer", true);

            customerStateManager.agent.velocity = Vector3.zero;
            customerStateManager.transform.LookAt(PlayerManager.Instance.transform.position);
        }

    }
}
