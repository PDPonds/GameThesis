using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class RestaurantManager : Auto_Singleton<RestaurantManager>
{
    public CustomerStateManager[] allCustomers;
    public EmployeeStateManager[] allEmployees;
    public TableObj[] allTables;

    public bool b_inProcess;

    [HideInInspector] public bool b_summaryButHasCustome;

    public int i_rating;
    public Vector2Int v_minmaxRating;
    public int i_ratingToRemove;
    public int i_ratingToAdd;

    private void Awake()
    {
        i_rating = v_minmaxRating.y;
    }

    void Update()
    {
        allCustomers = FindObjectsOfType<CustomerStateManager>();
        allEmployees = FindObjectsOfType<EmployeeStateManager>();
        allTables = FindObjectsOfType<TableObj>();

        if (AllEmployeeWorkingCheckProcess())
        {
            b_inProcess = true;
        }
        else
        {
            b_inProcess = false;
        }

        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_openState)
        {
            if (!AllTableIsFull())
            {
                for (int i = 0; i < allTables.Length; i++)
                {
                    if (allTables[i].b_isEmtry && allTables[i].b_readyForNextCustomer)
                    {
                        if (GetCustomerIndexCanOrder(out int customerIndex))
                        {
                            allCustomers[customerIndex].SwitchState(allCustomers[customerIndex].s_goToChairState);
                            allCustomers[customerIndex].c_tableObj = allTables[i];
                            allTables[i].b_isEmtry = false;
                            allTables[i].b_readyForNextCustomer = false;
                        }
                    }
                }
            }
        }

        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_summaryState)
        {
            if (RestaurantIsEmpty())
            {
                Debug.Log("Summary");
            }
        }

    }

    bool AllEmployeeWorkingCheckProcess()
    {
        int allEmployeeProcessing = 0;
        if (allEmployees.Length > 0)
        {
            for (int i = 0; i < allEmployees.Length; i++)
            {
                if (allEmployees[i].s_currentState == allEmployees[i].s_activityState)
                {
                    if (allEmployees[i].b_isWorking)
                    {
                        allEmployeeProcessing++;
                    }
                }
            }
        }

        return allEmployees.Length == allEmployeeProcessing;
    }

    bool AllTableIsFull()
    {
        if (allTables.Length > 0)
        {
            for (int i = 0; i < allTables.Length; i++)
            {
                if (allTables[i].b_isEmtry)
                {
                    return false;
                }
            }
        }
        return true;
    }

    bool GetCustomerIndexCanOrder(out int customerIndex)
    {
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].s_currentState == allCustomers[i].s_walkAroundState &&
                    !allCustomers[i].b_escape)
                {
                    customerIndex = i;
                    return true;
                }
            }
        }

        customerIndex = -1;
        return false;
    }

    public bool GetCanEmployeeServe(out int serveIndex)
    {
        if (allEmployees.Length > 0)
        {
            for (int i = 0; i < allEmployees.Length; i++)
            {
                if (allEmployees[i].employeeType == EmployeeType.Serve)
                {
                    if (allEmployees[i].b_canServe)
                    {
                        if (allEmployees[i].s_serveTable == null)
                        {
                            if (!CheckAIRepleat(allEmployees[i]))
                            {
                                serveIndex = i;
                                return true;
                            }
                        }
                    }
                }
            }
        }

        serveIndex = -1;
        return false;
    }

    public bool GetCurrentTableFormEmployee(EmployeeStateManager ai, out int tableFormEmployee)
    {
        if (allTables.Length > 0)
        {
            for (int i = 0; i < allTables.Length; i++)
            {
                if (allTables[i].s_currentEmployee == ai)
                {
                    tableFormEmployee = i;
                    return true;
                }
            }
        }
        tableFormEmployee = -1;
        return false;
    }

    bool CheckAIRepleat(EmployeeStateManager ai)
    {
        if (allTables.Length > 0)
        {
            for (int i = 0; i < allTables.Length; i++)
            {
                if (allTables[i].s_currentEmployee == ai)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool RestaurantIsEmpty()
    {
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].s_currentState == allCustomers[i].s_eatFoodState ||
                    allCustomers[i].s_currentState == allCustomers[i].s_waitFoodState ||
                    allCustomers[i].s_currentState == allCustomers[i].s_goToCounterState ||
                    allCustomers[i].s_currentState == allCustomers[i].s_goToChairState ||
                    allCustomers[i].s_currentState == allCustomers[i].s_frontCounter ||
                    allCustomers[i].s_currentState == allCustomers[i].s_fightState ||
                    allCustomers[i].s_currentState == allCustomers[i].s_attackState ||
                    allCustomers[i].b_escape)
                {
                    b_summaryButHasCustome = true;
                    return false;
                }
            }
        }
        b_summaryButHasCustome = false;
        return true;
    }

    public bool SomeOneEscape()
    {
        if (allCustomers.Length >= 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].b_escape)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool AllEmployeeWorkingCheckForText()
    {
        int allEmployeeProcessing = 0;
        if (allEmployees.Length > 0)
        {
            for (int i = 0; i < allEmployees.Length; i++)
            {
                if (allEmployees[i].s_currentState == allEmployees[i].s_activityState)
                {
                    allEmployeeProcessing++;

                }
            }
        }

        return allEmployees.Length == allEmployeeProcessing;
    }

    public bool HasCustomerInFightState()
    {
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].s_currentState == allCustomers[i].s_fightState ||
                    allCustomers[i].s_currentState == allCustomers[i].s_attackState)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void RemoveRating()
    {
        i_rating -= i_ratingToRemove;
        if (i_rating <= v_minmaxRating.x)
        {
            i_rating = v_minmaxRating.x;
        }
    }

    public void AddRating()
    {
        i_rating += i_ratingToAdd;
        if (i_rating >= v_minmaxRating.y)
        {
            i_rating = v_minmaxRating.y;
        }
    }

}
