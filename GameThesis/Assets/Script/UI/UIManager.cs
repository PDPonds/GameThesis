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

    private void Update()
    {
        text_coin.text = $"$ : {GameManager.Instance.f_coin}";

        text_time.text = TimeController.Instance.d_currentTime.ToString("HH:mm");
    }
}
