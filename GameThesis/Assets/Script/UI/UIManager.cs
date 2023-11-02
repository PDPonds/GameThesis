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
    public GameObject g_wayPointUI;
    //public List<WaypointIndicator> escapeWayPoint = new List<WaypointIndicator>();

    private void Update()
    {
        text_Cash.text = $"{GameManager.Instance.f_coin.ToString("00.00")}$";

        text_time.text = TimeController.Instance.d_currentTime.ToString("HH:mm");

        #region warning

        if (!RestaurantManager.Instance.AllEmployeeWorkingCheckForText()) g_slockOff.SetActive(true);
        else g_slockOff.SetActive(false);

        if (RestaurantManager.Instance.SomeOneEscape())
        {
            g_escape.SetActive(true);
            //int escapeCount = RestaurantManager.Instance.GetEscapeCount();
            //if (escapeWayPoint.Count != escapeCount)
            //{
            //    foreach (WaypointIndicator wayPoint in escapeWayPoint)
            //    { Destroy(wayPoint.gameObject); }

            //    escapeWayPoint.Clear();
            //    if (RestaurantManager.Instance.allCustomers.Length > 0)
            //    {
            //        for (int i = 0; i < RestaurantManager.Instance.allCustomers.Length; i++)
            //        {
            //            CustomerStateManager cus = RestaurantManager.Instance.allCustomers[i];
            //            if (cus.b_escape)
            //            {
            //                SpawnWayPoint(cus);
            //            }
            //        }
            //    }

            //}
        }
        else
        {
            //foreach (WaypointIndicator wayPoint in escapeWayPoint)
            //{ Destroy(wayPoint.gameObject); }
            //escapeWayPoint.Clear();

            g_escape.SetActive(false);
        }

        if (RestaurantManager.Instance.HasGangToTeachYou()) g_gang.SetActive(true);
        else g_gang.SetActive(false);

        if(RestaurantManager.Instance.SomeOneSleep()) g_drunkSleep.SetActive(true);
        else g_drunkSleep.SetActive(false);

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

        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_closeState)
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


    public void SpawnWayPoint(CustomerStateManager cus)
    {
        GameObject wayPointObj = Instantiate(g_wayPointUI, Vector3.zero, Quaternion.identity);
        wayPointObj.transform.SetParent(t_Canvas);
        WaypointIndicator indicator = wayPointObj.GetComponent<WaypointIndicator>();
        indicator.target = cus.t_mesh;
        //escapeWayPoint.Add(indicator);
    }

}
