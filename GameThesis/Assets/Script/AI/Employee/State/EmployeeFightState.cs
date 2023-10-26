using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeFightState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.ApplyOutlineColor(employeeStateManager.color_fighting, employeeStateManager.f_outlineScale);
        employeeStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;

        employeeStateManager.RagdollOff();

        employeeStateManager.DisablePunch();

        if (!employeeStateManager.b_canAtk)
        {
            employeeStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (employeeStateManager.f_currentAtkDelay <= 0)
            {
                employeeStateManager.b_canAtk = true;
            }
        }


        employeeStateManager.agent.SetDestination(PlayerManager.Instance.transform.position);

        Collider[] player = Physics.OverlapSphere(ai.transform.position, employeeStateManager.f_runRange, GameManager.Instance.lm_playerMask);
        if (player.Length > 0)
        {
            employeeStateManager.agent.speed = employeeStateManager.f_walkSpeed;

            employeeStateManager.anim.SetBool("fightState", true);
            employeeStateManager.anim.SetBool("walk", false);
            employeeStateManager.anim.SetBool("run", false);
            employeeStateManager.anim.SetBool("sit", false);
            employeeStateManager.anim.SetBool("drunk", false);

            if (Vector3.Distance(PlayerManager.Instance.transform.position, employeeStateManager.transform.position) <=
                employeeStateManager.f_atkRange)
            {
                employeeStateManager.agent.velocity = Vector2.zero;
                if (employeeStateManager.b_canAtk)
                {
                    employeeStateManager.SwitchState(employeeStateManager.s_attackState);
                }
            }

        }
        else
        {
            employeeStateManager.agent.speed = employeeStateManager.f_runSpeed;
            employeeStateManager.anim.SetBool("fightState", false);
            employeeStateManager.anim.SetBool("walk", false);
            employeeStateManager.anim.SetBool("run", true);
            employeeStateManager.anim.SetBool("sit", false);
            employeeStateManager.anim.SetBool("drunk", false);
        }


    }
}
