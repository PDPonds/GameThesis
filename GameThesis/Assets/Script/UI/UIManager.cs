using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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

    [Header("===== Time =====")]
    public TextMeshProUGUI text_time;
    [Space(10f)]

    [Header("===== Warning =====")]
    public GameObject g_escape;
    public GameObject g_slockOff;
    public GameObject g_gang;
    public GameObject g_drunkSleep;
    [Space(10f)]

    [Header("===== Rating =====")]
    public TextMeshProUGUI text_rating;
    [Space(10f)]

    [Header("===== Leave Area Text =====")]
    public GameObject text_leaveArea;
    public bool b_leaveTrigger;
    [Space(10f)]

    [Header("===== Close Warning ====")]
    public GameObject g_closeWarning;
    [Space(10f)]

    [Header("===== Objective =====")]
    public GameObject g_objective;
    public TextMeshProUGUI text_objectiveName;
    public TextMeshProUGUI text_objectiveDis;
    [Space(10f)]

    [Header("===== WayPoint =====")]
    public GameObject g_cashWaypoint;
    public GameObject g_sleepWaypoint;
    public GameObject g_sneakWaypoint;
    public GameObject g_threatWaypoint;
    public GameObject g_slackoffWaypoint;
    List<WaypointIndicator> allSneakWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allSleepWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allCashWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allThreatWaypoint = new List<WaypointIndicator>();
    List<WaypointIndicator> allSlackOffWaypoint = new List<WaypointIndicator>();

    [Header("===== Summary =====")]
    public GameObject g_summary;

    private void Update()
    {
        text_Cash.text = $"{GameManager.Instance.f_coin.ToString("00.00")}$";

        text_time.text = TimeController.Instance.d_currentTime.ToString("HH:mm");

        #region warning

        if (!RestaurantManager.Instance.AllEmployeeWorkingCheckForText())
        {
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
            g_gang.SetActive(true);
        }
        else
        {
            g_gang.SetActive(false);
        }

        if (RestaurantManager.Instance.SomeOneSleep())
        {
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

        text_rating.text = $"{RestaurantManager.Instance.i_rating} / {RestaurantManager.Instance.v_minmaxRating.y}";

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
            g_closeWarning.SetActive(true);
        }
        else
        {
            g_closeWarning.SetActive(false);
        }

        if (ObjectiveManager.Instance.GetCurrentObjective(out int index) &&
            index < ObjectiveManager.Instance.allObjectives.Count)
        {
            g_objective.SetActive(true);
            ObjectiveSO objective = ObjectiveManager.Instance.allObjectives[index];
            text_objectiveName.text = objective.s_objectiveName;
            text_objectiveDis.text = objective.s_objectiveDescription;
        }
        else
        {
            g_objective.SetActive(false);
        }

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
}
