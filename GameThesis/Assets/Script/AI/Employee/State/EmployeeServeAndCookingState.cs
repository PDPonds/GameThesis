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
        f_currentToSlowTime = employeeStateManager.f_timeToSlackOff;
        employeeStateManager.anim.SetBool("fightState", false);

        Color noColor = new Color(0, 0, 0, 0);
        employeeStateManager.ApplyOutlineColor(noColor, 0f);
        employeeStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;

        employeeStateManager.RagdollOff();

        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_openState)
        {
            switch (employeeStateManager.employeeType)
            {
                case EmployeeType.Cooking:

                    if (Vector3.Distance(employeeStateManager.transform.position, employeeStateManager.t_workingPos.position)
                        <= 1f)
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

                    if (RestaurantManager.Instance.b_inProcess)
                    {
                        if (RestaurantManager.Instance.GetCurrentChairFormEmployee(employeeStateManager, out int chairIndex))
                        {
                            ChairObj chair = RestaurantManager.Instance.allChairs[chairIndex];

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
                                employeeStateManager.agent.SetDestination(chair.transform.position);
                                if (Vector3.Distance(employeeStateManager.transform.position, chair.transform.position)
                                    <= 2f)
                                {
                                    employeeStateManager.b_hasFood = false;
                                    employeeStateManager.b_canServe = false;
                                    employeeStateManager.s_serveChair = null;
                                    chair.s_currentCustomer.SwitchState(chair.s_currentCustomer.s_eatFoodState);
                                }
                            }

                            employeeStateManager.agent.speed = employeeStateManager.f_walkSpeed;
                            employeeStateManager.anim.SetBool("walk", true);
                            employeeStateManager.anim.SetBool("run", false);

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
                                employeeStateManager.anim.SetBool("run", false);
                            }
                            else
                            {
                                employeeStateManager.anim.SetBool("walk", true);
                                employeeStateManager.agent.speed = employeeStateManager.f_walkSpeed;
                                employeeStateManager.anim.SetBool("run", false);

                            }
                            employeeStateManager.b_hasFood = false;
                            employeeStateManager.s_serveChair = null;
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
                            employeeStateManager.anim.SetBool("run", false);
                        }
                        else
                        {
                            employeeStateManager.anim.SetBool("walk", false);
                            employeeStateManager.agent.speed = employeeStateManager.f_runSpeed;
                            employeeStateManager.anim.SetBool("run", true);
                        }

                        employeeStateManager.b_hasFood = false;
                        employeeStateManager.b_canServe = true;
                        employeeStateManager.s_serveChair = null;

                    }

                    if (employeeStateManager.b_hasFood)
                    {
                        employeeStateManager.anim.SetLayerWeight(1, 1);
                        employeeStateManager.g_FoodInHand.SetActive(true);
                    }
                    else
                    {
                        employeeStateManager.anim.SetLayerWeight(1, 0);
                        employeeStateManager.g_FoodInHand.SetActive(false);
                    }

                    employeeStateManager.b_isWorking = true;

                    break;
                default: break;
            }

            f_currentToSlowTime -= Time.deltaTime;
            if (f_currentToSlowTime <= 0)
            {
                float p = Random.Range(0f, 100f);
                if (p <= employeeStateManager.f_slackOffPercent)
                {
                    employeeStateManager.SwitchState(employeeStateManager.s_slowDownState);
                    f_currentToSlowTime = employeeStateManager.f_timeToSlackOff;
                }
                else
                {
                    f_currentToSlowTime = employeeStateManager.f_timeToSlackOff;
                }
            }
        }
        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_closeState)
        {
            switch (employeeStateManager.employeeType)
            {
                case EmployeeType.Cooking:

                    employeeStateManager.agent.SetDestination(employeeStateManager.t_workingPos.position);

                    employeeStateManager.anim.SetBool("run", true);
                    employeeStateManager.anim.SetBool("walk", false);
                    employeeStateManager.agent.speed = employeeStateManager.f_runSpeed;

                    if (Vector3.Distance(employeeStateManager.transform.position, employeeStateManager.t_workingPos.position)
                        <= 1f)
                    {
                        employeeStateManager.agent.velocity = Vector3.zero;
                        employeeStateManager.anim.SetBool("run", false);
                        employeeStateManager.anim.SetBool("walk", false);
                    }

                    break;
                case EmployeeType.Serve:

                    if (RestaurantManager.Instance.b_summaryButHasCustome)
                    {
                        if (RestaurantManager.Instance.GetCurrentChairFormEmployee(employeeStateManager, out int chairIndex))
                        {
                            ChairObj chair = RestaurantManager.Instance.allChairs[chairIndex];

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
                                employeeStateManager.agent.SetDestination(chair.transform.position);
                                if (Vector3.Distance(employeeStateManager.transform.position, chair.transform.position)
                                    <= 2f)
                                {
                                    employeeStateManager.b_hasFood = false;
                                    employeeStateManager.b_canServe = false;
                                    employeeStateManager.s_serveChair = null;
                                    chair.s_currentCustomer.SwitchState(chair.s_currentCustomer.s_eatFoodState);
                                }
                            }

                            employeeStateManager.agent.speed = employeeStateManager.f_walkSpeed;
                            employeeStateManager.anim.SetBool("walk", true);
                            employeeStateManager.anim.SetBool("run", false);

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
                                employeeStateManager.anim.SetBool("run", false);
                            }
                            else
                            {
                                employeeStateManager.anim.SetBool("walk", false);
                                employeeStateManager.agent.speed = employeeStateManager.f_runSpeed;
                                employeeStateManager.anim.SetBool("run", true);

                            }
                            employeeStateManager.b_hasFood = false;
                            employeeStateManager.s_serveChair = null;
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
                            employeeStateManager.anim.SetBool("run", false);
                        }
                        else
                        {
                            employeeStateManager.anim.SetBool("walk", true);
                            employeeStateManager.agent.speed = employeeStateManager.f_walkSpeed;
                            employeeStateManager.anim.SetBool("run", false);
                        }
                    }

                    if (employeeStateManager.b_hasFood)
                    {
                        employeeStateManager.anim.SetLayerWeight(1, 1);
                        employeeStateManager.g_FoodInHand.SetActive(true);
                    }
                    else
                    {
                        employeeStateManager.anim.SetLayerWeight(1, 0);
                        employeeStateManager.g_FoodInHand.SetActive(false);
                    }

                    break;

                default: break;
            }
        }


    }

}