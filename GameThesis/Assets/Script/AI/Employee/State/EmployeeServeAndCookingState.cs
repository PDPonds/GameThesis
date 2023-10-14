using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum ServerState
{
    None, GoToBar, GoToTable, GoToWaitPoint
}

public class EmployeeServeAndCookingState : BaseState
{

    public ServerState serverState = ServerState.None;

    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.i_currentHP = employeeStateManager.i_maxHP;
        employeeStateManager.b_isWorking = false;
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;

        employeeStateManager.DisablePunch();
        employeeStateManager.RagdollOff();

        switch (employeeStateManager.employeeType)
        {
            case EmployeeType.Cooking:

                if (Vector3.Distance(employeeStateManager.transform.position, employeeStateManager.t_workingPos.position)
                    <= 0.5f)
                {
                    employeeStateManager.b_isWorking = true;
                    employeeStateManager.agent.velocity = Vector3.zero;
                    employeeStateManager.anim.SetBool("walk", false);
                }
                else
                {
                    employeeStateManager.b_isWorking = false;
                }

                if (!employeeStateManager.b_isWorking)
                {
                    employeeStateManager.agent.SetDestination(employeeStateManager.t_workingPos.position);
                    employeeStateManager.anim.SetBool("walk", true);
                }

                break;
            case EmployeeType.Serve:

                if (RestaurantManager.Instance.b_inProcess)
                {
                    if (RestaurantManager.Instance.GetCurrentTableFormEmployee(employeeStateManager, out int tableIndex))
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
                                employeeStateManager.b_canServe = true;
                                employeeStateManager.s_serveTable = null;
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
                        employeeStateManager.b_hasFood = false;
                        employeeStateManager.b_canServe = true;
                        employeeStateManager.s_serveTable = null;
                    }

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

                    employeeStateManager.b_hasFood = false;
                    employeeStateManager.b_canServe = true;
                    employeeStateManager.s_serveTable = null;
                }

                employeeStateManager.b_isWorking = true;

                break;
            default: break;
        }

        employeeStateManager.anim.SetBool("fightState", false);

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