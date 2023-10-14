using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableObj : MonoBehaviour
{
    public GameObject g_table;
    public List<GameObject> g_chairs = new List<GameObject>();

    public float f_dirtyCount;

    public bool b_isEmtry;

    public float f_cdForNextCustomer;
    [HideInInspector] public bool b_readyForNextCustomer;
    float f_currentCDForNextCustomer;

    public CustomerStateManager s_currentCustomer;
    public EmployeeStateManager s_currentEmployee;

    public Image img_icon;
    public Image img_progressBar;
    public Sprite sprit_waitFood;

    private void Awake()
    {
        b_isEmtry = true;
    }

    private void Update()
    {
        Debug.Log(s_currentEmployee + transform.name);

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

        if (s_currentCustomer != null)
        {
            if (s_currentCustomer.s_currentState == s_currentCustomer.s_waitFoodState)
            {
                img_progressBar.enabled = true;
                img_icon.enabled = true;
                img_icon.sprite = sprit_waitFood;

                float progressTime = s_currentCustomer.f_currentOrderTime / s_currentCustomer.f_orderTime;

                img_progressBar.color = new Color(1 - progressTime, progressTime, 0, 1);

                img_progressBar.fillAmount = progressTime;

                if (RestaurantManager.Instance.GetCanEmployeeServe(out int index) &&
                    s_currentEmployee == null)
                {
                    RestaurantManager.Instance.allEmployees[index].s_serveTable = this;
                    RestaurantManager.Instance.allEmployees[index].b_canServe = false;
                    s_currentEmployee = RestaurantManager.Instance.allEmployees[index];
                }
            }
            else
            {
                img_progressBar.enabled = false;
                img_icon.enabled = false;

            }


        }
        else
        {
            img_progressBar.enabled = false;
            img_icon.enabled = false;

        }

        if (s_currentCustomer == null || s_currentCustomer.s_currentState != s_currentCustomer.s_waitFoodState)
        {
            s_currentEmployee = null;
        }

    }

}
