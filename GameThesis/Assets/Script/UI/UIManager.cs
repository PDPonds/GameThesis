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
    public TextMeshProUGUI text_warning;

    [Header("===== Rating =====")]
    public TextMeshProUGUI text_rating;

    [Header("===== Leave Area Text =====")]
    public GameObject text_leaveArea;
    public bool b_leaveTrigger;
    private void Update()
    {
        text_coin.text = $"$ : {GameManager.Instance.f_coin.ToString("00.00")}";

        text_time.text = TimeController.Instance.d_currentTime.ToString("HH:mm");

        #region warning

        text_warning.text = GetWarningText();
        text_warning.color = Color.yellow;

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

    }

    string GetWarningText()
    {
        string text = string.Empty;

        if (!RestaurantManager.Instance.AllEmployeeWorkingCheckForText())
        {
            text += $"• Someone slacks off. !!{Environment.NewLine}";
        }

        if (RestaurantManager.Instance.SomeOneEscape())
        {
            text += $"• Someone Escape. !! {Environment.NewLine}";
        }

        return text;
    }

}
