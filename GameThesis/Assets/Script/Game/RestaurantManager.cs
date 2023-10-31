using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RestaurantManager : Auto_Singleton<RestaurantManager>
{
    public CustomerStateManager[] allCustomers;
    public EmployeeStateManager[] allEmployees;
    public TableObj[] allTables;
    public SheriffStateManager[] allSheriffs;
    public ChairObj[] allChairs;

    public bool b_inProcess;

    [HideInInspector] public bool b_summaryButHasCustome;

    [Header("===== Start Rating =====")]
    public int i_rating;
    public Vector2Int v_minmaxRating;
    public int i_startRating;
    public int i_ratingToRemove;
    public int i_ratingToAdd;
    public UI_animmationController uiAnimCon;

    [Header("===== Upgrade Table Manager =====")]
    public int i_startTable;

    [Header("===== Level =====")]
    public int i_level = 1;

    private void Awake()
    {
        i_rating = i_startRating;
    }

    private void Start()
    {
        for (int i = 0; i < i_startTable; i++)
        {
            UpgradTable up = allTables[i].transform.GetComponent<UpgradTable>();
            up.b_readyToUse = true;
            up.SetUpUseAble();
        }
    }

    void Update()
    {
        allCustomers = FindObjectsOfType<CustomerStateManager>();
        allEmployees = FindObjectsOfType<EmployeeStateManager>();
        allSheriffs = FindObjectsOfType<SheriffStateManager>();
        allTables = FindObjectsOfType<TableObj>();
        allChairs = FindObjectsOfType<ChairObj>();

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
            if (!AllChairIsFull())
            {
                for (int i = 0; i < allChairs.Length; i++)
                {
                    if (allChairs[i].b_isEmpty && allChairs[i].b_readyForNextCustomer && allChairs[i].b_canUse)
                    {
                        if (GetCustomerIndexCanOrder(out int customerIndex))
                        {
                            allCustomers[customerIndex].SwitchState(allCustomers[customerIndex].s_goToChairState);
                            allCustomers[customerIndex].c_chairObj = allChairs[i];
                            allChairs[i].b_isEmpty = false;
                            allChairs[i].b_readyForNextCustomer = false;
                        }
                    }
                }
            }

        }

        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_closeState)
        {
            if (RestaurantIsEmpty())
            {

            }
        }

    }

    public int ReqRateToBuyTable()
    {
        int rating = 0;

        int currentStep = (allTableIsReady() - i_startTable) + 1;
        rating = currentStep * 10;
        return rating;
    }

    int allTableIsReady()
    {
        int ready = 0;
        for (int i = 0; i < allTables.Length; i++)
        {
            UpgradTable up = allTables[i].transform.GetComponent<UpgradTable>();
            if (up.b_readyToUse)
            {
                ready++;
            }
        }
        return ready;
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

    bool AllChairIsFull()
    {
        if (allChairs.Length > 0)
        {
            for (int i = 0; i < allChairs.Length; i++)
            {
                if (allChairs[i].b_isEmpty)
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
                        if (allEmployees[i].s_serveChair == null)
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

    public bool GetCurrentChairFormEmployee(EmployeeStateManager ai, out int tableFormEmployee)
    {
        if (allChairs.Length > 0)
        {
            for (int i = 0; i < allChairs.Length; i++)
            {
                if (allChairs[i].s_currentEmployee == ai)
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
        if (allChairs.Length > 0)
        {
            for (int i = 0; i < allChairs.Length; i++)
            {
                if (allChairs[i].s_currentEmployee == ai)
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
                if (allCustomers[i].b_inFight)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //public bool HasGangToTeachYou()
    //{
    //    for (int i = 0; i < allCustomers.Length; i++)
    //    {
    //        if (allCustomers[i].s_currentState == allCustomers[i].s_aggressive)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    public void RemoveRating()
    {
        uiAnimCon.RemoveRatingAnim(); // Waann

        i_rating -= i_ratingToRemove;
        if (i_rating <= v_minmaxRating.x)
        {
            i_rating = v_minmaxRating.x;
        }
    }

    public void AddRating()
    {
        uiAnimCon.AddRatingAnim(); //Waann

        i_rating += i_ratingToAdd;
        if (i_rating >= v_minmaxRating.y)
        {
            i_rating = v_minmaxRating.y;
        }
    }

    public int GetCurrentTableIsReadyCount()
    {
        int redyCount = 0;

        for (int i = 0; i < allTables.Length; i++)
        {
            UpgradTable up = allTables[i].transform.GetComponent<UpgradTable>();
            if (up.b_readyToUse) redyCount++;
        }

        return redyCount;
    }

}
