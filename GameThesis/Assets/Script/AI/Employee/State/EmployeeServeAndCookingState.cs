using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EmployeeServeAndCookingState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.i_currentHP = employeeStateManager.i_maxHP;
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        switch (employeeStateManager.employeeType)
        {
            case EmployeeType.Cooking:



                break;
            case EmployeeType.Serve:

                if (RestaurantManager.Instance.GetTableCanServe(out int tableIndex) &&
                    RestaurantManager.Instance.allTables[tableIndex].s_currentCustomer.s_currentState ==
                    RestaurantManager.Instance.allTables[tableIndex].s_currentCustomer.s_waitFoodState)
                {
                    TableObj table = RestaurantManager.Instance.allTables[tableIndex];
                    if (!employeeStateManager.b_hasFood)
                    {
                        employeeStateManager.agent.SetDestination(GameManager.Instance.t_getFoodPos.position);
                        if (Vector3.Distance(employeeStateManager.transform.position, GameManager.Instance.t_getFoodPos.position)
                            <= 1f)
                        {
                            employeeStateManager.b_hasFood = true;
                        }
                    }
                    else
                    {
                        employeeStateManager.agent.SetDestination(table.transform.position);
                        if (Vector3.Distance(employeeStateManager.transform.position, table.transform.position)
                            <= 2f)
                        {
                            employeeStateManager.b_hasFood = false;
                            table.s_currentCustomer.SwitchState(table.s_currentCustomer.s_eatFoodState);
                        }
                    }

                    employeeStateManager.anim.SetBool("walk", true);
                }
                else
                {
                    employeeStateManager.agent.SetDestination(GameManager.Instance.t_stayPos.position);
                    if (Vector3.Distance(employeeStateManager.transform.position, GameManager.Instance.t_stayPos.position)
                       <= 1f)
                    {
                        employeeStateManager.agent.velocity = Vector3.zero;
                        employeeStateManager.anim.SetBool("walk", false);
                    }
                    else
                    {
                        employeeStateManager.anim.SetBool("walk", true);
                    }
                }

                break;
            default: break;
        }

        employeeStateManager.anim.SetBool("fightState", false);

        employeeStateManager.DisablePunch();
        employeeStateManager.RagdollOff();

        if (!employeeStateManager.b_canAtk)
        {
            employeeStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (employeeStateManager.f_currentAtkDelay <= 0)
            {
                employeeStateManager.b_canAtk = true;
            }
        }
    }

}