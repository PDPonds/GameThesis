using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeFightState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;

        employeeStateManager.RagdollOff();

        employeeStateManager.agent.SetDestination(PlayerManager.Instance.transform.position);

        employeeStateManager.anim.SetBool("fightState", true);

        employeeStateManager.DisablePunch();

        if (!employeeStateManager.b_canAtk)
        {
            employeeStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (employeeStateManager.f_currentAtkDelay <= 0)
            {
                employeeStateManager.b_canAtk = true;
            }
        }

        Collider[] player = Physics.OverlapSphere(ai.transform.position, employeeStateManager.f_atkRange, GameManager.Instance.lm_playerMask);
        if (player.Length > 0)
        {
            employeeStateManager.agent.velocity = Vector2.zero;
            if (employeeStateManager.b_canAtk)
            {
                employeeStateManager.SwitchState(employeeStateManager.s_attackState);
            }
        }

    }
}
