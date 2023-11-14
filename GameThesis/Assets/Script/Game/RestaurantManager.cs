using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RestaurantManager : Auto_Singleton<RestaurantManager>
{
    public CustomerStateManager[] allCustomers;
    public EmployeeStateManager[] allEmployees;
    public TableObj[] allTables;
    public SheriffStateManager[] allSheriffs;
    public ChairObj[] allChairs;

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

    [Header("===== Manage Restaurant ======")]
    [Header("- Cost")]
    public float f_cookerCost;
    public float f_waiterCost;

    [HideInInspector] public float f_currentCostPerDay;

    [Header("- Setup")]
    public int i_maxCooker;
    public int i_maxWaiter;

    public int i_minCooker;
    public int i_minWaiter;

    [HideInInspector] public int i_currentCookerCount;
    [HideInInspector] public int i_currentWaiterCount;
    [Header("- Prefabs")]
    public GameObject g_cooker;
    public GameObject g_waiter;

    [Header("- CookingPos")]
    public List<Transform> allCookingPos = new List<Transform>();


    [Header("- Menu")]
    public MenuHandler menuHandler;

    public void AddCurrentCookingCount()
    {
        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (i_currentCookerCount < i_maxCooker)
            {
                i_currentCookerCount++;
            }
        }

    }

    public void AddCurrentServeCount()
    {
        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (i_currentWaiterCount < i_maxWaiter)
            {
                i_currentWaiterCount++;
            }
        }
    }

    public void RemoveCurrentCookingCount()
    {
        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (i_currentCookerCount > i_minCooker)
            {
                i_currentCookerCount--;

            }
        }
    }

    public void RemoveCurrentServeCount()
    {
        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (i_currentWaiterCount > i_minWaiter)
            {
                i_currentWaiterCount--;

            }
        }
    }

    public void SpawnEmp()
    {
        if (GameManager.Instance.s_gameState.s_currentState ==
            GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (i_currentCookerCount > 0)
            {
                for (int i = 0; i < i_currentCookerCount; i++)
                {
                    Vector3 cookingPos = allCookingPos[i].position;
                    GameObject cookingEmpObj = Instantiate(g_cooker, cookingPos, Quaternion.identity);
                    EmployeeStateManager emp = cookingEmpObj.GetComponent<EmployeeStateManager>();
                    emp.t_workingPos = allCookingPos[i];
                }
            }

            if (i_currentWaiterCount > 0)
            {
                for (int i = 0; i < i_currentWaiterCount; i++)
                {
                    Vector3 severPos = GameManager.Instance.t_stayPos.position;
                    GameObject serveEmpObj = Instantiate(g_waiter, severPos, Quaternion.identity);
                }
            }
        }

    }


    private void Awake()
    {
        i_currentCookerCount = i_minCooker;
        i_currentWaiterCount = i_minWaiter;

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
        float allCookerCost = i_currentCookerCount * f_cookerCost;
        float allWaiterCost = i_currentWaiterCount * f_waiterCost;
        f_currentCostPerDay = allCookerCost + allWaiterCost;

        allCustomers = FindObjectsOfType<CustomerStateManager>();
        allEmployees = FindObjectsOfType<EmployeeStateManager>();
        allSheriffs = FindObjectsOfType<SheriffStateManager>();
        allTables = FindObjectsOfType<TableObj>();
        allChairs = FindObjectsOfType<ChairObj>();

        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            UIManager.Instance.g_summary.SetActive(false);
            PlayerManager.Instance.b_canMove = true;

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

            UIManager.Instance.g_summary.SetActive(false);
            PlayerManager.Instance.b_canMove = true;

        }

    }

    public void CloseRestaurant()
    {
        if (allCustomers.Length > 0)
        {
            foreach (CustomerStateManager cus in allCustomers)
            {
                if (cus.s_currentState != cus.s_walkAroundState)
                    cus.SwitchState(cus.s_walkAroundState);
            }
        }

        if (allEmployees.Length > 0)
        {
            foreach (EmployeeStateManager emp in allEmployees)
            {
                Destroy(emp.gameObject);
            }
        }

        UIManager.Instance.g_summary.SetActive(true);

        PlayerManager.Instance.b_canMove = false;
    }

    public void ClearChair()
    {
        if (allChairs.Length > 0)
        {
            for (int i = 0; i < allChairs.Length; i++)
            {
                if (allChairs[i].s_currentCustomer != null)
                {
                    allChairs[i].s_currentCustomer = null;
                }

                if (allChairs[i].s_currentCookingEmployee != null)
                {
                    allChairs[i].s_currentCookingEmployee = null;
                }

                if (allChairs[i].s_currentServerEmployee != null)
                {
                    allChairs[i].s_currentServerEmployee = null;
                }

                allChairs[i].b_isEmpty = true;
                allChairs[i].b_readyForNextCustomer = true;
                allChairs[i].b_canUse = true;
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
                            if (!CheckServeAIRepleat(allEmployees[i]))
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

    public bool GetCanEmployeeCooking(out int cookingIndex)
    {
        if (allEmployees.Length > 0)
        {
            for (int i = 0; i < allEmployees.Length; i++)
            {
                if (allEmployees[i].employeeType == EmployeeType.Cooking)
                {
                    if (allEmployees[i].b_canCook)
                    {
                        if (allEmployees[i].s_cookingChair == null)
                        {
                            if (!CheckCookingAIRepleat(allEmployees[i]))
                            {
                                cookingIndex = i;
                                return true;
                            }
                        }
                    }
                }
            }
        }

        cookingIndex = -1;
        return false;
    }

    public bool GetCurrentChairFormServeEmployee(EmployeeStateManager ai, out int chairFormEmployee)
    {
        if (allChairs.Length > 0)
        {
            for (int i = 0; i < allChairs.Length; i++)
            {
                if (allChairs[i].s_currentServerEmployee == ai)
                {
                    chairFormEmployee = i;
                    return true;
                }
            }
        }
        chairFormEmployee = -1;
        return false;
    }

    bool CheckServeAIRepleat(EmployeeStateManager ai)
    {
        if (allChairs.Length > 0)
        {
            for (int i = 0; i < allChairs.Length; i++)
            {
                if (allChairs[i].s_currentServerEmployee == ai)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool CheckCookingAIRepleat(EmployeeStateManager ai)
    {
        if (allChairs.Length > 0)
        {
            for (int i = 0; i < allChairs.Length; i++)
            {
                if (allChairs[i].s_currentCookingEmployee == ai)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool RestaurantIsEmpty()
    {
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                CustomerStateManager cus = allCustomers[i];
                if (cus.s_currentState == cus.s_eatFoodState ||
                    cus.s_currentState == cus.s_waitFoodState ||
                    cus.s_currentState == cus.s_goToCounterState ||
                    cus.s_currentState == cus.s_goToChairState ||
                    cus.s_currentState == cus.s_frontCounter ||
                    cus.s_currentState == cus.s_fightState ||
                    cus.s_currentState == cus.s_drunkState ||
                    cus.s_currentState == cus.s_giveBackState ||
                    cus.b_escape)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool SomeOneCash()
    {
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                CustomerStateManager cus = allCustomers[i];
                if (cus.s_currentState == cus.s_frontCounter)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int GetCashCount()
    {
        int count = 0;
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                CustomerStateManager cus = allCustomers[i];
                if (cus.s_currentState == cus.s_frontCounter)
                {
                    count++;
                }
            }
        }
        return count;
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

    public int GetEscapeCount()
    {
        int count = 0;
        if (allCustomers.Length >= 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].b_escape)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public bool SomeOneSleep()
    {
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].s_currentState == allCustomers[i].s_drunkState)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int GetSleepCount()
    {
        int count = 0;
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].s_currentState == allCustomers[i].s_drunkState)
                {
                    count++;
                }
            }
        }
        return count;
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

    public int GetSlackoffCount()
    {
        int count = 0;
        if (allEmployees.Length > 0)
        {
            for (int i = 0; i < allEmployees.Length; i++)
            {
                if (allEmployees[i].s_currentState == allEmployees[i].s_slackOffState)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public bool HasCustomerInFightState()
    {
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].b_inFight ||
                    allCustomers[i].s_currentState == allCustomers[i].s_fightState)
                {
                    return true;
                }
            }
        }
        return false;
    }

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

    public bool HasGangToTeachYou()
    {
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].b_isGang)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool HasThreat()
    {
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                CustomerStateManager cus = allCustomers[i];
                if (cus.b_fightWithPlayer == true)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int GetThreatCount()
    {
        int count = 0;
        if (allCustomers.Length > 0)
        {
            for (int i = 0; i < allCustomers.Length; i++)
            {
                if (allCustomers[i].b_fightWithPlayer)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
