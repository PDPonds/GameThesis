using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObj : MonoBehaviour
{
    public GameObject g_table;
    public List<GameObject> g_chairs = new List<GameObject>();

    public float f_dirtyCount;

    public bool b_isEmtry;

    public float f_cdForNextCustomer;
    [HideInInspector] public bool b_readyForNextCustomer;
    float f_currentCDForNextCustomer;

    private void Awake()
    {
        b_isEmtry = true;
    }

    private void Update()
    {
        if (b_isEmtry && !b_readyForNextCustomer)
        {
            f_currentCDForNextCustomer -= Time.deltaTime;
            if (f_currentCDForNextCustomer <= 0)
            {
                b_readyForNextCustomer = true;
            }
        }
        else if (b_readyForNextCustomer || !b_isEmtry)
        {
            f_currentCDForNextCustomer = f_cdForNextCustomer;
        }

    }

}
