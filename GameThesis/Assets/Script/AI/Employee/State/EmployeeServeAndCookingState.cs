using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EmployeeServeAndCookingState : BaseState
{
    public float f_currentToSlowTime;

    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.i_currentHP = employeeStateManager.i_maxHP;
        employeeStateManager.b_isWorking = false;
        f_currentToSlowTime = employeeStateManager.f_timeToSlow;
        employeeStateManager.anim.SetBool("fightState", false);
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
                    employeeStateManager.anim.SetBool("run", false);
                    employeeStateManager.anim.SetBool("walk", false);
                }
                else
                {
                    employeeStateManager.b_isWorking = false;
                }

                if (!employeeStateManager.b_isWorking)
                {
                    employeeStateManager.agent.SetDestination(employeeStateManager.t_workingPos.position);
                    employeeStateManager.anim.SetBool("run", true);
                    employeeStateManager.anim.SetBool("walk", false);
                }

                employeeStateManager.agent.speed = employeeStateManager.f_runSpeed;

                break;
            case EmployeeType.Serve:

                employeeStateManager.agent.speed = employeeStateManager.f_walkSpeed;

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
                                employeeStateManager.b_canServe = false;
                            }
                        }
                        else
                        {
                            employeeStateManager.agent.SetDestination(table.transform.position);
                            if (Vector3.Distance(employeeStateManager.transform.position, table.transform.position)
                                <= 2f)
                            {
                                employeeStateManager.b_hasFood = false;
                                employeeStateManager.b_canServe = false;
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
                            employeeStateManager.b_canServe = true;
                            employeeStateManager.anim.SetBool("walk", false);
                        }
                        else
                        {
                            employeeStateManager.anim.SetBool("walk", true);
                        }
                        employeeStateManager.b_hasFood = false;
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

        if (!employeeStateManager.b_canAtk)
        {
            employeeStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (employeeStateManager.f_currentAtkDelay <= 0)
            {
                employeeStateManager.b_canAtk = true;
            }
        }

        f_currentToSlowTime -= Time.deltaTime;
        if (f_currentToSlowTime <= 0)
        {
            float p = Random.Range(0f, 100f);
            if (p <= employeeStateManager.f_slowPercent)
            {
                employeeStateManager.SwitchState(employeeStateManager.s_slowDownState);
                f_currentToSlowTime = employeeStateManager.f_timeToSlow;
            }
            else
            {
                f_currentToSlowTime = employeeStateManager.f_timeToSlow;
            }
        }

    }

}