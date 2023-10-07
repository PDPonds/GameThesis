using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : Auto_Singleton<PlayerUI>
{
    public Material m_bloodMat;
    public Transform t_bloodObj;

    private void Update()
    {
        if (PlayerManager.Instance.i_currentHP < PlayerManager.Instance.i_maxHP)
        {
            t_bloodObj.gameObject.SetActive(true);
            m_bloodMat.SetFloat("_Range", 1.5f + PlayerManager.Instance.i_currentHP);
        }
        else
        {
            t_bloodObj.gameObject.SetActive(false);
        }
    }
}
