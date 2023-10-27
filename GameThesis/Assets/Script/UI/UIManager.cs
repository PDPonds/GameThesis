using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Auto_Singleton<UIManager>
{
    [Header("===== Coin =====")]
    public TextMeshProUGUI text_coin;

    [Header("===== Time =====")]
    public TextMeshProUGUI text_time;

    [Header("===== Warning =====")]
    public GameObject g_escape;
    public GameObject g_slockOff;
    public GameObject g_gang;

    [Header("===== Rating =====")]
    public TextMeshProUGUI text_rating;

    [Header("===== Leave Area Text =====")]
    public GameObject text_leaveArea;
    public bool b_leaveTrigger;

    [Header("===== Close Warning ====")]
    public GameObject g_closeWarning;

    [Header("===== Objective =====")]
    public GameObject g_objective;
    public TextMeshProUGUI text_objectiveName;
    public TextMeshProUGUI text_objectiveDis;

    private void Update()
    {
        text_coin.text = $"$ : {GameManager.Instance.f_coin.ToString("00.00")}";

        text_time.text = TimeController.Instance.d_currentTime.ToString("HH:mm");

        #region warning

        if (!RestaurantManager.Instance.AllEmployeeWorkingCheckForText()) g_slockOff.SetActive(true);
        else g_slockOff.SetActive(false);

        if (RestaurantManager.Instance.SomeOneEscape()) g_escape.SetActive(true);
        else g_escape.SetActive(false);

        if (RestaurantManager.Instance.HasGangToTeachYou()) g_gang.SetActive(true);
        else g_gang.SetActive(false);



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


}
