using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableObj : MonoBehaviour
{
    public GameObject g_table;
    public List<GameObject> g_chairs = new List<GameObject>();

    private void Update()
    {
        if (transform.TryGetComponent<UpgradTable>(out UpgradTable up))
        {
            if (up.b_readyToUse)
            {
                if (g_chairs.Count > 0)
                {
                    for (int i = 0; i < g_chairs.Count; i++)
                    {
                        ChairObj chair = g_chairs[i].GetComponent<ChairObj>();
                        chair.b_canUse = true;
                    }
                }
            }
            else
            {
                if (g_chairs.Count > 0)
                {
                    for (int i = 0; i < g_chairs.Count; i++)
                    {
                        ChairObj chair = g_chairs[i].GetComponent<ChairObj>();
                        chair.b_canUse = false;
                    }
                }
            }
        }
        else
        {
            if (g_chairs.Count > 0)
            {
                for (int i = 0; i < g_chairs.Count; i++)
                {
                    ChairObj chair = g_chairs[i].GetComponent<ChairObj>();
                    chair.b_canUse = false;
                }
            }
        }
    }
}
