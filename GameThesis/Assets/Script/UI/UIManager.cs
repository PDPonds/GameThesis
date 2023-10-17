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

    private void Update()
    {
        text_coin.text = $"$ : {GameManager.Instance.f_coin}";

        text_time.text = TimeController.Instance.d_currentTime.ToString("HH:mm");

        #region warning

        text_warning.text = GetWarningText();
        text_warning.color = Color.red;

        #endregion

    }

    string GetWarningText()
    {
        string text = string.Empty;

        if (!RestaurantManager.Instance.AllEmployeeWorkingCheckForText())
        {
            text += $"Someone slacks off. !!{Environment.NewLine}";
        }

        if (RestaurantManager.Instance.SomeOneEscape())
        {
            text += $"Someone Escape. !! {Environment.NewLine}";
        }

        return text;
    }

}
