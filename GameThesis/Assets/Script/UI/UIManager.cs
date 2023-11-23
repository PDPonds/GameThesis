using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Auto_Singleton<UIManager>
{

    public Transform t_Canvas;

    [Header("===== Coin =====")]
    public TextMeshProUGUI text_Cash;
    public TextMeshProUGUI addCash;
    public TextMeshProUGUI removeCash;
    public UI_animmationController uiAnimCon;
    [Space(10f)]

    [Header("===== PocketMoney =====")]
    public TextMeshProUGUI text_pocketCash;
    public TextMeshProUGUI addPocketCash;
    public TextMeshProUGUI removePocketCash;
    public UI_animmationController uiAnimPocket;
    [Space(10f)]

    [Header("===== Time =====")]
    public TextMeshProUGUI text_day;
    public TextMeshProUGUI text_time;
    [Space(10f)]

    [Header("===== Warning =====")]
    public GameObject g_escape;
    public GameObject g_slockOff;
    public GameObject g_gang;
    public GameObject g_drunkSleep;
    [Space(10f)]

    [Header("===== Rating =====")]
    //public TextMeshProUGUI text_rating;
    [Space(10f)]

    [Header("===== Leave Area Text =====")]
    public GameObject text_leaveArea;
    public bool b_leaveTrigger;
    [Space(10f)]

    [Header("===== Objective ====")]
    public GameObject g_open;
    public GameObject g_close;
    public GameObject g_goToBoardForWaiter;
    public GameObject g_goToCounter;
    public GameObject g_firstQ;
    public GameObject g_goToBoardForCooker;
    public GameObject g_unlockBeer;
    public GameObject g_unlockTable;
    [Space(10f)]

    [Header("===== WayPoint =====")]
    public GameObject g_cashWaypoint;
    public GameObject g_sleepWaypoint;
    public GameObject g_sneakWaypoint;
    public GameObject g_threatWaypoint;
    public GameObject g_slackoffWaypoint;
    public GameObject g_helpCookerWaypoint;

    List<WaypointIndicator> allSneakWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allSleepWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allCashWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allThreatWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allSlackOffWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allHelpCookerWaypoint = new List<WaypointIndicator>();

    public GameObject g_openAndCloseRestaurantWaypoint;
    public GameObject g_managementBoardWaypoint;
    public GameObject g_menuWaypoint;

    List<WaypointIndicator> allOpenAndCloseWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allManagementWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allMenuWaypoint = new List<WaypointIndicator>();

    public Transform t_managementBoardMesh;
    public Transform t_doorMesh;
    public Transform t_menuBoardMesh;

    [Header("===== Summary =====")]
    public GameObject g_summary;
    public Button button_next;
    public TextMeshProUGUI text_waiterCount;
    public TextMeshProUGUI text_cookerCount;
    public TextMeshProUGUI text_profitCost;
    public TextMeshProUGUI text_daliyIncomeCost;
    public TextMeshProUGUI text_totalCostPerDay;
    public TextMeshProUGUI text_waiterCost;
    public TextMeshProUGUI text_cookerCost;
    public TextMeshProUGUI text_ingredientCost;

    [Header("===== Lose Win =====")]
    public GameObject winPage;
    public GameObject losePage;
    public Button winBut;
    public Button loseBut;

    [Header("===== Letter =====")]
    public GameObject letter;
    public GameObject letterUI;

    [Header("===== Parent Bar =====")]
    public GameObject UIBar;
    public GameObject ObjectiveBar;
    public GameObject TutorialImage;

    private void Awake()
    {
        button_next.onClick.AddListener(() => SkipDay());
        winBut.onClick.AddListener(() => WinButton());
        loseBut.onClick.AddListener(() => LoseButton());
    }

    void WinButton()
    {
        winPage.SetActive(false);
    }

    void LoseButton()
    {
        losePage.SetActive(false);
        SceneManager.LoadScene(0);
    }

    void SkipDay()
    {
        StartCoroutine(PlayerManager.Instance.SkipDay());
        if (GameManager.Instance.f_coin >= RestaurantManager.Instance.f_currentCostPerDay)
        {
            float toAddPocket = GameManager.Instance.f_coin - RestaurantManager.Instance.f_currentCostPerDay;
            GameManager.Instance.AddPocketMoney(toAddPocket);
            GameManager.Instance.f_coin = 0;
            RestaurantManager.Instance.i_currentWaiterCount = 1;
            if (GameManager.Instance.i_currentDay > 2)
            {
                RestaurantManager.Instance.i_currentCookerCount = 1;
            }

        }
        else
        {
            float toRemovePocket = RestaurantManager.Instance.f_currentCostPerDay - GameManager.Instance.f_coin;
            GameManager.Instance.RemovePocketMoney(toRemovePocket);
            GameManager.Instance.f_coin = 0;
            RestaurantManager.Instance.i_currentWaiterCount = 1;
            if (GameManager.Instance.i_currentDay > 2)
            {
                RestaurantManager.Instance.i_currentCookerCount = 1;
            }
        }
        RestaurantManager.Instance.ClearChair();
        TimeController.Instance.ResetTime();
        GameManager.Instance.i_currentDay++;
        RestaurantManager.Instance.ResetIngredietnCost();
        GameManager.Instance.s_gameState.SwitchState(GameManager.Instance.s_gameState.s_beforeOpenState);
    }

    private void Update()
    {
        text_day.text = $"Day : {GameManager.Instance.i_currentDay}";

        text_Cash.text = $"{GameManager.Instance.f_coin.ToString("F2")}$";
        text_pocketCash.text = $"{GameManager.Instance.f_pocketMoney.ToString("F2")}$";

        text_time.text = TimeController.Instance.d_currentTime.ToString("HH:mm");

        #region warning

        if (!RestaurantManager.Instance.AllEmployeeWorkingCheckForText())
        {
            g_slockOff.transform.SetAsLastSibling();
            g_slockOff.SetActive(true);
            int slackOffCount = RestaurantManager.Instance.GetSlackoffCount();
            if (allSlackOffWaypoint.Count != slackOffCount)
            {
                foreach (WaypointIndicator waypoints in allSlackOffWaypoint)
                { Destroy(waypoints.gameObject); }
                allSlackOffWaypoint.Clear();

                for (int i = 0; i < RestaurantManager.Instance.allEmployees.Length; i++)
                {
                    EmployeeStateManager emp = RestaurantManager.Instance.allEmployees[i];
                    if (emp.b_onSlackOffPoint)
                    {
                        WaypointIndicator waypoint = SpawnWayPoint(g_slackoffWaypoint, emp);
                        allSlackOffWaypoint.Add(waypoint);
                    }
                }
            }
        }
        else
        {
            g_slockOff.SetActive(false);

            foreach (WaypointIndicator waypoints in allSlackOffWaypoint)
            { Destroy(waypoints.gameObject); }
            allSlackOffWaypoint.Clear();
        }

        if (RestaurantManager.Instance.SomeOneEscape())
        {
            g_escape.transform.SetAsLastSibling();
            g_escape.SetActive(true);
            int escapeCount = RestaurantManager.Instance.GetEscapeCount();
            if (allSneakWaypoint.Count != escapeCount)
            {
                foreach (WaypointIndicator wayPoint in allSneakWaypoint)
                { Destroy(wayPoint.gameObject); }

                allSneakWaypoint.Clear();
                if (RestaurantManager.Instance.allCustomers.Length > 0)
                {
                    for (int i = 0; i < RestaurantManager.Instance.allCustomers.Length; i++)
                    {
                        CustomerStateManager cus = RestaurantManager.Instance.allCustomers[i];
                        if (cus.b_escape)
                        {
                            WaypointIndicator waypoint = SpawnWayPoint(g_sneakWaypoint, cus);
                            allSneakWaypoint.Add(waypoint);
                        }
                    }
                }
            }

        }
        else
        {
            if (allSneakWaypoint.Count > 0)
            {
                foreach (WaypointIndicator wayPoint in allSneakWaypoint)
                { Destroy(wayPoint.gameObject); }
                allSneakWaypoint.Clear();
            }

            g_escape.SetActive(false);
        }

        if (RestaurantManager.Instance.HasGangToTeachYou())
        {
            g_slockOff.transform.SetAsLastSibling();
            g_gang.SetActive(true);
        }
        else
        {
            g_gang.SetActive(false);
        }

        if (RestaurantManager.Instance.SomeOneSleep())
        {
            g_drunkSleep.transform.SetAsLastSibling();
            g_drunkSleep.SetActive(true);
            int sleepCount = RestaurantManager.Instance.GetSleepCount();
            if (allSleepWaypoint.Count != sleepCount)
            {
                foreach (WaypointIndicator waypoint in allSleepWaypoint)
                { Destroy(waypoint.gameObject); }
                allSleepWaypoint.Clear();
                if (RestaurantManager.Instance.allCustomers.Length > 0)
                {
                    for (int i = 0; i < RestaurantManager.Instance.allCustomers.Length; i++)
                    {
                        CustomerStateManager cus = RestaurantManager.Instance.allCustomers[i];
                        if (cus.s_currentState == cus.s_drunkState)
                        {
                            WaypointIndicator waypoint = SpawnWayPoint(g_sleepWaypoint, cus);
                            allSleepWaypoint.Add(waypoint);
                        }
                    }
                }
            }

        }
        else
        {
            if (allSleepWaypoint.Count > 0)
            {
                foreach (WaypointIndicator waypoint in allSleepWaypoint)
                { Destroy(waypoint.gameObject); }
                allSleepWaypoint.Clear();
            }
            g_drunkSleep.SetActive(false);
        }

        if (RestaurantManager.Instance.SomeOneCash())
        {
            int cashCount = RestaurantManager.Instance.GetCashCount();
            if (allCashWaypoint.Count != cashCount)
            {
                foreach (WaypointIndicator waypoint in allCashWaypoint)
                { Destroy(waypoint.gameObject); }
                allCashWaypoint.Clear();
                if (RestaurantManager.Instance.allCustomers.Length > 0)
                {
                    for (int i = 0; i < RestaurantManager.Instance.allCustomers.Length; i++)
                    {
                        CustomerStateManager cus = RestaurantManager.Instance.allCustomers[i];
                        if (cus.s_currentState == cus.s_frontCounter)
                        {
                            WaypointIndicator waypoint = SpawnWayPoint(g_cashWaypoint, cus);
                            allCashWaypoint.Add(waypoint);
                        }
                    }
                }
            }
        }
        else
        {
            if (allCashWaypoint.Count > 0)
            {
                foreach (WaypointIndicator waypoint in allCashWaypoint)
                { Destroy(waypoint.gameObject); }
                allCashWaypoint.Clear();
            }

        }

        if (RestaurantManager.Instance.HasThreat())
        {
            int threatCount = RestaurantManager.Instance.GetThreatCount();
            if (allThreatWaypoint.Count != threatCount)
            {
                foreach (WaypointIndicator waypoint in allThreatWaypoint)
                { Destroy(waypoint.gameObject); }
                allThreatWaypoint.Clear();
                if (RestaurantManager.Instance.allCustomers.Length > 0)
                {
                    for (int i = 0; i < RestaurantManager.Instance.allCustomers.Length; i++)
                    {
                        CustomerStateManager cus = RestaurantManager.Instance.allCustomers[i];
                        if (cus.b_fightWithPlayer)
                        {
                            WaypointIndicator waypoint = SpawnWayPoint(g_threatWaypoint, cus);
                            allThreatWaypoint.Add(waypoint);
                        }
                    }
                }
            }
        }
        else
        {
            if (allThreatWaypoint.Count > 0)
            {
                foreach (WaypointIndicator waypoint in allThreatWaypoint)
                { Destroy(waypoint.gameObject); }
                allThreatWaypoint.Clear();
            }
        }


        #endregion

        //text_rating.text = $"{RestaurantManager.Instance.i_rating} / {RestaurantManager.Instance.v_minmaxRating.y}";

        if (b_leaveTrigger)
        {
            text_leaveArea.SetActive(true);
        }
        else
        {
            text_leaveArea.SetActive(false);

        }

        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (GameManager.Instance.i_currentDay >= 3)
            {
                SpawnManagementBoardWaypoint();
                SpawnMenuBoardWaypoint();
                SpawnOpenCloseWaypoint();
            }
        }
        else if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_afterOpenState)
        {
            text_waiterCount.text = $"x {RestaurantManager.Instance.i_currentWaiterCount}";
            text_cookerCount.text = $"x {RestaurantManager.Instance.i_currentCookerCount}";
            text_daliyIncomeCost.text = $"{GameManager.Instance.f_coin.ToString("F2")}$";
            float cookerCost = RestaurantManager.Instance.i_currentCookerCount * RestaurantManager.Instance.f_cookerCost;
            float waiterCost = RestaurantManager.Instance.i_currentWaiterCount * RestaurantManager.Instance.f_waiterCost;
            text_totalCostPerDay.text = $"- {RestaurantManager.Instance.f_currentCostPerDay.ToString("F2")}$";
            float profit = GameManager.Instance.f_coin - RestaurantManager.Instance.f_currentCostPerDay;
            text_profitCost.text = $"{profit.ToString("F2")}$";
            text_waiterCost.text = $"{waiterCost.ToString("F2")}$";
            text_cookerCost.text = $"{cookerCost.ToString("F2")}$";
            text_ingredientCost.text = $"{RestaurantManager.Instance.f_ingredientCost.ToString("F2")}$";

            if (RestaurantManager.Instance.RestaurantIsEmpty())
            {
                SpawnOpenCloseWaypoint();
                g_close.SetActive(true);
            }
            else
            {
                g_close.SetActive(false);

                if (RestaurantManager.Instance.currentPotAndPan != null)
                {
                    SpawnHelpCooker();
                }
                else
                {
                    DestroyHelpCooker();
                }
            }

        }
        else
        {

            if (RestaurantManager.Instance.currentPotAndPan != null)
            {
                SpawnHelpCooker();
            }
            else
            {
                DestroyHelpCooker();
            }

            if (GameManager.Instance.i_currentDay >= 3)
            {

                DestroyManagementBoardWaypoint();
                DestroyMenuBoardWaypoint();
                DestroyOpenClseWaypoint();
            }

        }

        if (letter.gameObject.activeSelf) letterUI.SetActive(true);
        else letterUI.SetActive(false);

    }

    public void SpawnHelpCooker()
    {
        if (allHelpCookerWaypoint.Count != 1)
        {
            foreach (WaypointIndicator waypoint in allHelpCookerWaypoint)
            {
                Destroy(waypoint.gameObject);
            }
            allHelpCookerWaypoint.Clear();

            GameObject wayPointObj = Instantiate(g_helpCookerWaypoint, Vector3.zero, Quaternion.identity);
            wayPointObj.transform.SetParent(t_Canvas);
            WaypointIndicator indicator = wayPointObj.GetComponent<WaypointIndicator>();
            indicator.target = RestaurantManager.Instance.currentPotAndPan.transform;

            allHelpCookerWaypoint.Add(indicator);
        }
    }

    public void DestroyHelpCooker()
    {
        if (allHelpCookerWaypoint.Count > 0)
        {
            foreach (WaypointIndicator waypoint in allHelpCookerWaypoint)
            {
                Destroy(waypoint.gameObject);
            }
        }
        allHelpCookerWaypoint.Clear();
    }

    public void SpawnOpenCloseWaypoint()
    {
        if (allOpenAndCloseWaypoint.Count != 1)
        {
            foreach (WaypointIndicator waypoint in allOpenAndCloseWaypoint)
            {
                Destroy(waypoint.gameObject);
            }
            allOpenAndCloseWaypoint.Clear();
            GameObject wayPointObj = Instantiate(g_openAndCloseRestaurantWaypoint, Vector3.zero, Quaternion.identity);
            wayPointObj.transform.SetParent(t_Canvas);
            WaypointIndicator indicator = wayPointObj.GetComponent<WaypointIndicator>();
            indicator.target = t_doorMesh;
            allOpenAndCloseWaypoint.Add(indicator);
        }
    }

    public void DestroyOpenClseWaypoint()
    {
        if (allOpenAndCloseWaypoint.Count > 0)
        {
            foreach (WaypointIndicator waypoint in allOpenAndCloseWaypoint)
            {
                Destroy(waypoint.gameObject);
            }
        }
        allOpenAndCloseWaypoint.Clear();
    }

    public void SpawnManagementBoardWaypoint()
    {
        if (allManagementWaypoint.Count != 1)
        {
            foreach (WaypointIndicator waypoint in allManagementWaypoint)
            {
                Destroy(waypoint.gameObject);
            }
            allManagementWaypoint.Clear();
            GameObject wayPointObj = Instantiate(g_managementBoardWaypoint, Vector3.zero, Quaternion.identity);
            wayPointObj.transform.SetParent(t_Canvas);
            WaypointIndicator indicator = wayPointObj.GetComponent<WaypointIndicator>();
            indicator.target = t_managementBoardMesh;
            allManagementWaypoint.Add(indicator);
        }
    }

    public void DestroyManagementBoardWaypoint()
    {
        if (allManagementWaypoint.Count > 0)
        {
            foreach (WaypointIndicator waypoint in allManagementWaypoint)
            {
                Destroy(waypoint.gameObject);
            }
        }
        allManagementWaypoint.Clear();
    }

    public void SpawnMenuBoardWaypoint()
    {
        if (allMenuWaypoint.Count != 1)
        {
            foreach (WaypointIndicator waypoint in allMenuWaypoint)
            {
                Destroy(waypoint.gameObject);
            }
            allMenuWaypoint.Clear();
            GameObject wayPointObj = Instantiate(g_menuWaypoint, Vector3.zero, Quaternion.identity);
            wayPointObj.transform.SetParent(t_Canvas);
            WaypointIndicator indicator = wayPointObj.GetComponent<WaypointIndicator>();
            indicator.target = t_menuBoardMesh;
            allMenuWaypoint.Add(indicator);
        }
    }

    public void DestroyMenuBoardWaypoint()
    {
        if (allMenuWaypoint.Count > 0)
        {
            foreach (WaypointIndicator waypoint in allMenuWaypoint)
            {
                Destroy(waypoint.gameObject);
            }
        }
        allMenuWaypoint.Clear();
    }


    public WaypointIndicator SpawnWayPoint(GameObject waypoint, CustomerStateManager cus)
    {
        GameObject wayPointObj = Instantiate(waypoint, Vector3.zero, Quaternion.identity);
        wayPointObj.transform.SetParent(t_Canvas);
        WaypointIndicator indicator = wayPointObj.GetComponent<WaypointIndicator>();
        indicator.target = cus.t_mesh;
        return indicator;
    }

    public WaypointIndicator SpawnWayPoint(GameObject waypoint, EmployeeStateManager emp)
    {
        GameObject wayPointObj = Instantiate(waypoint, Vector3.zero, Quaternion.identity);
        wayPointObj.transform.SetParent(t_Canvas);
        WaypointIndicator indicator = wayPointObj.GetComponent<WaypointIndicator>();
        indicator.target = emp.t_mesh;
        return indicator;
    }

    public WaypointIndicator SpawnWayPoint(GameObject waypoint, Transform pos)
    {
        GameObject wayPointObj = Instantiate(waypoint, Vector3.zero, Quaternion.identity);
        wayPointObj.transform.SetParent(t_Canvas);
        WaypointIndicator indicator = wayPointObj.GetComponent<WaypointIndicator>();
        indicator.target = pos;
        return indicator;
    }
}
