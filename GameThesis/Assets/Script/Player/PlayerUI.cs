using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : Auto_Singleton<PlayerUI>
{
    public Image img_blood;

    private void Update()
    {
        if (PlayerManager.Instance.i_currentHP < PlayerManager.Instance.i_maxHP)
        {
            img_blood.gameObject.SetActive(true);
            img_blood.color = new Color(img_blood.color.r, img_blood.color.g, img_blood.color.b, 1f / PlayerManager.Instance.i_currentHP);
        }
        else
        {
            img_blood.gameObject.SetActive(false);
        }
    }
}
