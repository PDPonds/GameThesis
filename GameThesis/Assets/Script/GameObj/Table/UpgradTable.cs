using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradTable : MonoBehaviour, IInteracable
{
    [HideInInspector] public bool b_readyToUse;
    public float f_costToBuy;

    [HideInInspector] public TableObj tableObj;
    public Material m_waitForBuy;
    public Material m_readyToUse;

    private void Awake()
    {
        tableObj = GetComponent<TableObj>();
        b_readyToUse = false;
        SetUpBuy();
    }

    public void SetUpUseAble()
    {
        MeshRenderer rndTable = tableObj.g_table.GetComponent<MeshRenderer>();
        rndTable.material = m_readyToUse;
        if (tableObj.g_chairs.Count > 0)
        {
            for (int i = 0; i < tableObj.g_chairs.Count; i++)
            {
                MeshRenderer rnd = tableObj.g_chairs[i].GetComponent<MeshRenderer>();
                if (rnd.material != m_readyToUse)
                {
                    rnd.material = m_readyToUse;
                }
            }
        }
        tableObj.enabled = true;
    }

    void SetUpBuy()
    {
        MeshRenderer rndTable = tableObj.g_table.GetComponent<MeshRenderer>();
        rndTable.material = m_waitForBuy;
        if (tableObj.g_chairs.Count > 0)
        {
            for (int i = 0; i < tableObj.g_chairs.Count; i++)
            {
                MeshRenderer rnd = tableObj.g_chairs[i].GetComponent<MeshRenderer>();
                if (rnd.material != m_waitForBuy)
                {
                    rnd.material = m_waitForBuy;
                }
            }
        }
        tableObj.enabled = false;
        if (tableObj.g_foods.Count > 0)
        {
            for (int i = 0; i < tableObj.g_foods.Count; i++)
            {
                if (tableObj.g_foods[i].activeSelf)
                {
                    tableObj.g_foods[i].SetActive(false);
                }
            }
        }

    }

    public void BuyTable()
    {
        if (GameManager.Instance.f_coin >= f_costToBuy &&
            RestaurantManager.Instance.i_rating >= RestaurantManager.Instance.ReqRateToBuyTable()
            && !b_readyToUse)
        {
            GameManager.Instance.RemoveCoin(f_costToBuy);
            SetUpUseAble();
            b_readyToUse = true;
        }
    }

    public void Interaction()
    {
        BuyTable();
    }

    public string InteractionText()
    {
        string text = string.Empty;
        if (!b_readyToUse)
        {
            text = $"[E] Use {f_costToBuy}$ And{Environment.NewLine} " +
                $"Atleast {RestaurantManager.Instance.ReqRateToBuyTable()} Rating to Unlock Table.";
        }
        return text;
    }

}
