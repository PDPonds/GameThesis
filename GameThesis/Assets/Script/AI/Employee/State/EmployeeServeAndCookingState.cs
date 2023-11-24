using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeServeAndCookingState : BaseState
{
    public float f_currentToSlowTime;
    float f_cookingTime;

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

        if (TutorialManager.Instance.currentTutorialIndex > 36)
            f_cookingTime = emp.f_cookingTime;
        else f_cookingTime = 5f;
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager emp = (EmployeeStateManager)ai;

        emp.RagdollOff();
        emp.b_onSlackOffPoint = false;

        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_openState)
        {
            switch (emp.employeeType)
            {
                case EmployeeType.Cooking:

                    emp.agent.SetDestination(emp.t_workingPos.position);

                    if (Vector3.Distance(emp.transform.position, emp.t_workingPos.position)
                        <= 0.1f)
                    {
                        emp.b_isWorking = true;
                        emp.anim.runtimeAnimatorController = emp.cookingAnim;
                        if (emp.indexPos == 0 || emp.indexPos == 2)
                        {
                            emp.transform.rotation = Quaternion.Euler(0, 90, 0);
                        }
                        else
                        {
                            emp.transform.rotation = Quaternion.Euler(0, -90, 0);
                        }
                        emp.agent.velocity = Vector3.zero;
                        emp.anim.SetBool("run", false);
                        emp.anim.SetBool("walk", false);

                        if (emp.b_canCook) emp.anim.SetBool("cooking", false);
                        else emp.anim.SetBool("cooking", true);

                        if (emp.s_cookingChair != null)
                        {
                            if (f_cookingTime == emp.f_cookingTime)
                            {
                                int dish = emp.s_cookingChair.s_currentCustomer.i_dish;
                                int drink = emp.s_cookingChair.s_currentCustomer.i_drink;

                                float dishCost = RestaurantManager.Instance.menuHandler.mainDish_Status[dish].cost;
                                float dirnkCost = RestaurantManager.Instance.menuHandler.drinks_Status[drink].cost;

                                float dishIng = dishCost / 2;
                                float drinkIng = dirnkCost / 2;

                                float IngCost = dishIng + drinkIng;

                                RestaurantManager.Instance.AddIngredientCost(IngCost);
                            }

                            emp.b_canCook = false;
                            f_cookingTime -= Time.deltaTime;
                            if (f_cookingTime < 0)
                            {
                                f_cookingTime = emp.f_cookingTime;
                                emp.b_canCook = true;
                                emp.s_cookingChair.b_finishCooking = true;
                                emp.s_cookingChair.s_currentCookingEmployee = null;
                                emp.s_cookingChair = null;
                            }
                        }

                    }
                    else
                    {
                        emp.b_isWorking = false;
                        emp.anim.SetBool("run", true);
                        emp.anim.SetBool("walk", false);
                        emp.anim.SetBool("cooking", false);
                    }


                    if (RestaurantManager.Instance.i_empLevel == 1)
                    {
                        emp.agent.speed = emp.f_runSpeed;
                    }
                    else if (RestaurantManager.Instance.i_empLevel == 2)
                    {
                        float empSpeed1 = emp.f_runSpeed * emp.f_level2Mul;
                        emp.agent.speed = empSpeed1;
                    }
                    else if (RestaurantManager.Instance.i_empLevel == 3)
                    {
                        float empSpeed2 = emp.f_runSpeed * emp.f_level3Mul;
                        emp.agent.speed = empSpeed2;
                    }


                    break;
                case EmployeeType.Serve:


                    if (RestaurantManager.Instance.GetCurrentChairFormServeEmployee(emp, out int chairIndex))
                    {

                        ChairObj chair = RestaurantManager.Instance.allChairs[chairIndex];
                        CustomerStateManager cus = chair.s_currentCustomer;

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

                        if (emp.b_hasFood)
                        {
                            emp.anim.SetLayerWeight(1, 1);
                            if (cus != null)
                            {
                                switch (cus.i_dish)
                                {
                                    case 0:
                                        emp.g_Bacon.SetActive(true);
                                        break;
                                    case 1:
                                        emp.g_Staw.SetActive(true);
                                        break;
                                    case 2:
                                        emp.g_Steak.SetActive(true);
                                        break;
                                }
                            }
                            else
                            {
                                emp.g_Steak.SetActive(false);
                                emp.g_Bacon.SetActive(false);
                                emp.g_Staw.SetActive(false);
                            }

                        }
                        else
                        {
                            emp.anim.SetLayerWeight(1, 0);
                            emp.g_Steak.SetActive(false);
                            emp.g_Bacon.SetActive(false);
                            emp.g_Staw.SetActive(false);
                        }

                        emp.anim.SetBool("walk", true);
                        emp.anim.SetBool("run", false);

                        if (RestaurantManager.Instance.i_empLevel == 1)
                        {
                            emp.agent.speed = emp.f_walkSpeed;
                        }
                        else if (RestaurantManager.Instance.i_empLevel == 2)
                        {
                            float empSpeed1 = emp.f_walkSpeed * emp.f_level2Mul;
                            emp.agent.speed = empSpeed1;
                        }
                        else if (RestaurantManager.Instance.i_empLevel == 3)
                        {
                            float empSpeed2 = emp.f_walkSpeed * emp.f_level3Mul;
                            emp.agent.speed = empSpeed2;
                        }
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
                            emp.anim.SetBool("run", false);

                            if (RestaurantManager.Instance.i_empLevel == 1)
                            {
                                emp.agent.speed = emp.f_walkSpeed;
                            }
                            else if (RestaurantManager.Instance.i_empLevel == 2)
                            {
                                float empSpeed1 = emp.f_walkSpeed * emp.f_level2Mul;
                                emp.agent.speed = empSpeed1;
                            }
                            else if (RestaurantManager.Instance.i_empLevel == 3)
                            {
                                float empSpeed2 = emp.f_walkSpeed * emp.f_level3Mul;
                                emp.agent.speed = empSpeed2;
                            }

                        }
                        emp.b_hasFood = false;
                        emp.s_serveChair = null;
                    }
                    emp.b_isWorking = true;

                    break;
                default: break;
            }

            if (TutorialManager.Instance.currentTutorialIndex > 33)
            {
                f_currentToSlowTime -= Time.deltaTime;
                if (f_currentToSlowTime <= 0)
                {
                    float p = Random.Range(0f, 100f);
                    if (p <= emp.f_slackOffPercent)
                    {
                        emp.SwitchState(emp.s_slackOffState);
                        f_currentToSlowTime = emp.f_timeToSlackOff;
                    }
                    else
                    {
                        f_currentToSlowTime = emp.f_timeToSlackOff;
                    }
                }
            }

        }
        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_afterOpenState)
        {
            switch (emp.employeeType)
            {
                case EmployeeType.Cooking:

                    if (!RestaurantManager.Instance.RestaurantIsEmpty())
                    {
                        emp.agent.SetDestination(emp.t_workingPos.position);

                        if (Vector3.Distance(emp.transform.position, emp.t_workingPos.position)
                            <= 0.1f)
                        {
                            emp.b_isWorking = true;
                            emp.anim.runtimeAnimatorController = emp.cookingAnim;
                            if (emp.indexPos == 0 || emp.indexPos == 2)
                            {
                                emp.transform.rotation = Quaternion.Euler(0, 90, 0);
                            }
                            else
                            {
                                emp.transform.rotation = Quaternion.Euler(0, -90, 0);
                            }
                            emp.agent.velocity = Vector3.zero;
                            emp.anim.SetBool("run", false);
                            emp.anim.SetBool("walk", false);
                            emp.anim.SetBool("cooking", true);

                            if (emp.s_cookingChair != null)
                            {
                                if (f_cookingTime == emp.f_cookingTime)
                                {
                                    int dish = emp.s_cookingChair.s_currentCustomer.i_dish;
                                    int drink = emp.s_cookingChair.s_currentCustomer.i_drink;

                                    float dishCost = RestaurantManager.Instance.menuHandler.mainDish_Status[dish].cost;
                                    float dirnkCost = RestaurantManager.Instance.menuHandler.drinks_Status[drink].cost;

                                    float dishIng = dishCost * RestaurantManager.Instance.ingredientCostMul;
                                    float drinkIng = dirnkCost * RestaurantManager.Instance.ingredientCostMul;

                                    float IngCost = dishIng + drinkIng;

                                    RestaurantManager.Instance.AddIngredientCost(IngCost);
                                }

                                emp.b_canCook = false;
                                f_cookingTime -= Time.deltaTime;
                                if (f_cookingTime < 0)
                                {
                                    f_cookingTime = emp.f_cookingTime;
                                    emp.b_canCook = true;
                                    emp.s_cookingChair.b_finishCooking = true;
                                    emp.s_cookingChair.s_currentCookingEmployee = null;
                                    emp.s_cookingChair = null;
                                }
                            }

                        }
                        else
                        {
                            emp.b_isWorking = false;
                            emp.anim.SetBool("run", true);
                            emp.anim.SetBool("walk", false);
                            emp.anim.SetBool("cooking", false);
                        }

                        if (RestaurantManager.Instance.i_empLevel == 1)
                        {
                            emp.agent.speed = emp.f_runSpeed;
                        }
                        else if (RestaurantManager.Instance.i_empLevel == 2)
                        {
                            float empSpeed1 = emp.f_runSpeed * emp.f_level2Mul;
                            emp.agent.speed = empSpeed1;
                        }
                        else if (RestaurantManager.Instance.i_empLevel == 3)
                        {
                            float empSpeed2 = emp.f_runSpeed * emp.f_level3Mul;
                            emp.agent.speed = empSpeed2;
                        }

                    }


                    break;
                case EmployeeType.Serve:
                    if (!RestaurantManager.Instance.RestaurantIsEmpty())
                    {
                        if (RestaurantManager.Instance.GetCurrentChairFormServeEmployee(emp, out int chairIndex))
                        {
                            if (RestaurantManager.Instance.i_empLevel == 1)
                            {
                                emp.agent.speed = emp.f_walkSpeed;
                            }
                            else if (RestaurantManager.Instance.i_empLevel == 2)
                            {
                                float empSpeed1 = emp.f_walkSpeed * emp.f_level2Mul;
                                emp.agent.speed = empSpeed1;
                            }
                            else if (RestaurantManager.Instance.i_empLevel == 3)
                            {
                                float empSpeed2 = emp.f_walkSpeed * emp.f_level3Mul;
                                emp.agent.speed = empSpeed2;
                            }

                            emp.anim.SetBool("walk", true);
                            emp.anim.SetBool("run", false);

                            ChairObj chair = RestaurantManager.Instance.allChairs[chairIndex];
                            CustomerStateManager cus = chair.s_currentCustomer;

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

                            if (emp.b_hasFood)
                            {
                                emp.anim.SetLayerWeight(1, 1);
                                switch (cus.i_dish)
                                {
                                    case 0:
                                        emp.g_Bacon.SetActive(true);
                                        break;
                                    case 1:
                                        emp.g_Staw.SetActive(true);
                                        break;
                                    case 2:
                                        emp.g_Steak.SetActive(true);
                                        break;
                                }

                            }
                            else
                            {
                                emp.anim.SetLayerWeight(1, 0);
                                emp.g_Steak.SetActive(false);
                                emp.g_Bacon.SetActive(false);
                                emp.g_Staw.SetActive(false);
                            }
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
                                if (RestaurantManager.Instance.i_empLevel == 1)
                                {
                                    emp.agent.speed = emp.f_walkSpeed;
                                }
                                else if (RestaurantManager.Instance.i_empLevel == 2)
                                {
                                    float empSpeed1 = emp.f_walkSpeed * emp.f_level2Mul;
                                    emp.agent.speed = empSpeed1;
                                }
                                else if (RestaurantManager.Instance.i_empLevel == 3)
                                {
                                    float empSpeed2 = emp.f_walkSpeed * emp.f_level3Mul;
                                    emp.agent.speed = empSpeed2;
                                }
                                emp.anim.SetBool("walk", true);
                                emp.anim.SetBool("run", false);

                            }
                            emp.b_hasFood = false;
                            emp.s_serveChair = null;
                        }
                    }

                    break;

                default: break;
            }
        }


    }

}