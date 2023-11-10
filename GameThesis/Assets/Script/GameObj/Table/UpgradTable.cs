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
    }

    private void Update()
    {
        SetUpBuy();
    }

    public void SetUpUseAble()
    {
        MeshRenderer rndTable = tableObj.g_table.GetComponent<MeshRenderer>();
        rndTable.material = m_readyToUse;

        Collider taCol = tableObj.g_table.GetComponent<Collider>();
        taCol.enabled = true;

        if (tableObj.g_chairs.Count > 0)
        {
            for (int i = 0; i < tableObj.g_chairs.Count; i++)
            {
                MeshRenderer rnd = tableObj.g_chairs[i].GetComponent<MeshRenderer>();
                if (rnd.material != m_readyToUse)
                {
                    rnd.material = m_readyToUse;
                }

                Collider chairCol = tableObj.g_chairs[i].GetComponent<Collider>();
                chairCol.enabled = true;
            }
        }
        tableObj.enabled = true;
    }

    public void SetUpBuy()
    {
        if (!b_readyToUse)
        {
            MeshRenderer rndTable = tableObj.g_table.GetComponent<MeshRenderer>();
            if (rndTable.material != m_readyToUse)
                rndTable.material = m_waitForBuy;

            GameState state = GameManager.Instance.s_gameState;

            if (state.s_currentState == state.s_beforeOpenState)
            {
                Collider[] taCol = tableObj.g_table.GetComponents<Collider>();
                foreach (Collider col in taCol)
                {
                    if (!col.enabled) col.enabled = true;
                }

                if (!tableObj.g_table.gameObject.activeSelf) tableObj.g_table.gameObject.SetActive(true);

                if (tableObj.g_chairs.Count > 0)
                {
                    for (int i = 0; i < tableObj.g_chairs.Count; i++)
                    {
                        MeshRenderer rnd = tableObj.g_chairs[i].GetComponent<MeshRenderer>();
                        if (rnd.material != m_waitForBuy)
                        {
                            rnd.material = m_waitForBuy;
                        }

                        Collider[] chairCol = tableObj.g_chairs[i].GetComponents<Collider>();
                        foreach (Collider col in chairCol) if (!col.enabled) col.enabled = true;

                        if (!tableObj.g_chairs[i].gameObject.activeSelf) tableObj.g_chairs[i].gameObject.SetActive(true);

                    }
                }
            }

            if (state.s_currentState == state.s_openState)
            {
                Collider[] taCol = tableObj.g_table.GetComponents<Collider>();
                foreach (Collider col in taCol)
                {
                    if (col.enabled) col.enabled = false;
                }

                if (tableObj.g_table.gameObject.activeSelf) tableObj.g_table.gameObject.SetActive(false);
                if (tableObj.g_chairs.Count > 0)
                {
                    for (int i = 0; i < tableObj.g_chairs.Count; i++)
                    {
                        MeshRenderer rnd = tableObj.g_chairs[i].GetComponent<MeshRenderer>();
                        if (rnd.material != m_waitForBuy)
                        {
                            rnd.material = m_waitForBuy;
                        }

                        Collider[] chairCol = tableObj.g_chairs[i].GetComponents<Collider>();
                        foreach (Collider col in chairCol) if (col.enabled) col.enabled = false;

                        if (tableObj.g_chairs[i].gameObject.activeSelf) tableObj.g_chairs[i].gameObject.SetActive(false);
                    }
                }
            }


        }

    }

    public void BuyTable()
    {
        if (GameManager.Instance.f_pocketMoney >= f_costToBuy &&
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
