using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Auto_Singleton<UIManager>
{
    public TextMeshProUGUI text_coin;

    private void Update()
    {
        text_coin.text = $"$ : {GameManager.Instance.f_coin}";
    }
}
