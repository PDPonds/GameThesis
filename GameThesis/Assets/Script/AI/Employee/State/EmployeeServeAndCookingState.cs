using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.Rendering.DebugUI;

public class EmployeeServeAndCookingState : BaseState
{
    public float f_currentToSlowTime;

    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager emp = (EmployeeStateManager)ai;
        emp.i_currentHP = emp.i_maxHP;
        emp.b_isWorking = false;
        f_currentToSlowTime = emp.f_timeToSlackOff;
        emp.anim.SetBool("fightState", false);
        emp.anim.SetBool("slackOff", false);

        Color noColor = new Color(0, 0, 0, 0);
        emp.ApplyOutlineColor(noColor, 0f);
        emp.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager emp = (EmployeeStateManager)ai;

        emp.RagdollOff();

        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_openState)
        {
            switch (emp.employeeType)
            {
                case EmployeeType.Cooking:

                    if (Vector3.Distance(emp.transform.position, emp.t_workingPos.position)
                        <= 1f)
                    {
                        emp.b_isWorking = true;
                        emp.agent.velocity = Vector3.zero;
                        emp.anim.SetBool("run", false);
                        emp.anim.SetBool("walk", false);
                        emp.anim.SetBool("cooking", true);
                        emp.anim.runtimeAnimatorController = emp.cookingAnim;
                    }
                    else
                    {
                        emp.b_isWorking = false;
                    }

                    if (!emp.b_isWorking)
                    {
                        emp.agent.SetDestination(emp.t_workingPos.position);
                        emp.anim.SetBool("run", true);
                        emp.anim.SetBool("walk", false);
                        emp.anim.SetBool("cooking", false);

                    }

                    emp.agent.speed = emp.f_runSpeed;

                    break;
                case EmployeeType.Serve:

                    if (RestaurantManager.Instance.b_inProcess)
                    {
                        if (RestaurantManager.Instance.GetCurrentChairFormEmployee(emp, out int chairIndex))
                        {
                            ChairObj chair = RestaurantManager.Instance.allChairs[chairIndex];

                            if (!emp.b_hasFood)
                            {
                                emp.agent.SetDestination(GameManager.Instance.t_getFoodPos.position);
                                if (Vector3.Distance(emp.transform.position, GameManager.Instance.t_getFoodPos.position)
                                    <= 1f)
                                {
                                    emp.b_hasFood = true;
                                    emp.b_canServe = false;
                                }
                            }
                            else
                            {
                                emp.agent.SetDestination(chair.transform.position);
                                if (Vector3.Distance(emp.transform.position, chair.transform.position)
                                    <= 2f)
                                {
                                    emp.b_hasFood = false;
                                    emp.b_canServe = false;
                                    emp.s_serveChair = null;
                                    chair.s_currentCustomer.SwitchState(chair.s_currentCustomer.s_eatFoodState);
                                }
                            }

                            emp.agent.speed = emp.f_walkSpeed;
                            emp.anim.SetBool("walk", true);
                            emp.anim.SetBool("run", false);

                        }
                        else
                        {
                            emp.agent.SetDestination(GameManager.Instance.t_stayPos.position);
                            if (Vector3.Distance(emp.transform.position, GameManager.Instance.t_stayPos.position)
                               <= 1f)
                            {
                                emp.agent.velocity = Vector3.zero;
                                emp.b_canServe = true;
                                emp.anim.SetBool("walk", false);
                                emp.anim.SetBool("run", false);
                            }
                            else
                            {
                                emp.anim.SetBool("walk", true);
                                emp.agent.speed = emp.f_walkSpeed;
                                emp.anim.SetBool("run", false);

                            }
                            emp.b_hasFood = false;
                            emp.s_serveChair = null;
                        }

                    }
                    else
                    {
                        emp.agent.SetDestination(GameManager.Instance.t_stayPos.position);
                        if (Vector3.Distance(emp.transform.position, GameManager.Instance.t_stayPos.position)
                           <= 1f)
                        {
                            emp.agent.velocity = Vector3.zero;
                            emp.anim.SetBool("walk", false);
                            emp.anim.SetBool("run", false);
                        }
                        else
                        {
                            emp.anim.SetBool("walk", false);
                            emp.agent.speed = emp.f_runSpeed;
                            emp.anim.SetBool("run", true);
                        }

                        emp.b_hasFood = false;
                        emp.b_canServe = true;
                        emp.s_serveChair = null;

                    }

                    if (emp.b_hasFood)
                    {
                        emp.anim.SetLayerWeight(1, 1);
                        emp.g_FoodInHand.SetActive(true);
                    }
                    else
                    {
                        emp.anim.SetLayerWeight(1, 0);
                        emp.g_FoodInHand.SetActive(false);
                    }

                    emp.b_isWorking = true;

                    break;
                default: break;
            }

            f_currentToSlowTime -= Time.deltaTime;
            if (f_currentToSlowTime <= 0)
            {
                float p = Random.Range(0f, 100f);
                if (p <= emp.f_slackOffPercent)
                {
                    emp.SwitchState(emp.s_slowDownState);
                    f_currentToSlowTime = emp.f_timeToSlackOff;
                }
                else
                {
                    f_currentToSlowTime = emp.f_timeToSlackOff;
                }
            }
        }
        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_closeState)
        {
            switch (emp.employeeType)
            {
                case EmployeeType.Cooking:

                    emp.agent.SetDestination(emp.t_workingPos.position);

                    emp.anim.SetBool("run", true);
                    emp.anim.SetBool("walk", false);
                    emp.agent.speed = emp.f_runSpeed;

                    if (Vector3.Distance(emp.transform.position, emp.t_workingPos.position)
                        <= 1f)
                    {
                        emp.agent.velocity = Vector3.zero;
                        emp.anim.SetBool("run", false);
                        emp.anim.SetBool("walk", false);
                    }

                    break;
                case EmployeeType.Serve:

                    if (RestaurantManager.Instance.b_summaryButHasCustome)
                    {
                        if (RestaurantManager.Instance.GetCurrentChairFormEmployee(emp, out int chairIndex))
                        {
                            ChairObj chair = RestaurantManager.Instance.allChairs[chairIndex];

                            if (!emp.b_hasFood)
                            {
                                emp.agent.SetDestination(GameManager.Instance.t_getFoodPos.position);
                                if (Vector3.Distance(emp.transform.position, GameManager.Instance.t_getFoodPos.position)
                                    <= 1f)
                                {
                                    emp.b_hasFood = true;
                                    emp.b_canServe = false;
                                }
                            }
                            else
                            {
                                emp.agent.SetDestination(chair.transform.position);
                                if (Vector3.Distance(emp.transform.position, chair.transform.position)
                                    <= 2f)
                                {
                                    emp.b_hasFood = false;
                                    emp.b_canServe = false;
                                    emp.s_serveChair = null;
                                    chair.s_currentCustomer.SwitchState(chair.s_currentCustomer.s_eatFoodState);
                                }
                            }

                            emp.agent.speed = emp.f_walkSpeed;
                            emp.anim.SetBool("walk", true);
                            emp.anim.SetBool("run", false);

                        }
                        else
                        {
                            emp.agent.SetDestination(GameManager.Instance.t_stayPos.position);
                            if (Vector3.Distance(emp.transform.position, GameManager.Instance.t_stayPos.position)
                               <= 1f)
                            {
                                emp.agent.velocity = Vector3.zero;
                                emp.b_canServe = true;
                                emp.anim.SetBool("walk", false);
                                emp.anim.SetBool("run", false);
                            }
                            else
                            {
                                emp.anim.SetBool("walk", false);
                                emp.agent.speed = emp.f_runSpeed;
                                emp.anim.SetBool("run", true);

                            }
                            emp.b_hasFood = false;
                            emp.s_serveChair = null;
                        }
                    }
                    else
                    {
                        emp.agent.SetDestination(GameManager.Instance.t_stayPos.position);
                        if (Vector3.Distance(emp.transform.position, GameManager.Instance.t_stayPos.position)
                           <= 1f)
                        {
                            emp.agent.velocity = Vector3.zero;
                            emp.anim.SetBool("walk", false);
                            emp.anim.SetBool("run", false);
                        }
                        else
                        {
                            emp.anim.SetBool("walk", true);
                            emp.agent.speed = emp.f_walkSpeed;
                            emp.anim.SetBool("run", false);
                        }
                    }

                    if (emp.b_hasFood)
                    {
                        emp.anim.SetLayerWeight(1, 1);
                        emp.g_FoodInHand.SetActive(true);
                    }
                    else
                    {
                        emp.anim.SetLayerWeight(1, 0);
                        emp.g_FoodInHand.SetActive(false);
                    }

                    break;

                default: break;
            }
        }


    }

}