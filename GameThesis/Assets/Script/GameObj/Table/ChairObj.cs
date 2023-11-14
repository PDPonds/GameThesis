using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairObj : MonoBehaviour
{
    public Transform t_sitPos;
    public Vector2 v_minmaxCDForNextCustomer;

    [HideInInspector] public bool b_isEmpty;

    [HideInInspector] public float f_cdForNextCustomer;

    [HideInInspector] public bool b_readyForNextCustomer;
    [HideInInspector] float f_currentCDForNextCustomer;

    [HideInInspector] public bool b_finishCooking;
    [HideInInspector] public CustomerStateManager s_currentCustomer;
    [HideInInspector] public EmployeeStateManager s_currentServerEmployee;
    [HideInInspector] public EmployeeStateManager s_currentCookingEmployee;

    TableObj table;
    [HideInInspector] public bool b_canUse;

    [Header("===== Food =====")]
    [Header("- Main Dish")]
    public GameObject g_steak;
    public GameObject g_stew;
    public GameObject g_beanAndBacon;
    [Header("- Drinks")]
    public GameObject g_beerGlass;
    public GameObject g_glass;

    private void Awake()
    {
        b_isEmpty = true;
        table = GetComponentInParent<TableObj>();
    }

    private void Update()
    {

        if (b_isEmpty && !b_readyForNextCustomer)
        {
            f_currentCDForNextCustomer -= Time.deltaTime;
            if (f_currentCDForNextCustomer <= 0)
            {
                b_readyForNextCustomer = true;
                RandomCDForNextCustomer();
            }
        }
        else if (b_readyForNextCustomer || !b_isEmpty)
        {
            f_currentCDForNextCustomer = f_cdForNextCustomer;
        }


        if (s_currentCustomer != null)
        {
            if (s_currentCustomer.c_chairObj != this)
            {
                s_currentCustomer = null;
                return;
            }

            if (s_currentCustomer.s_currentState == s_currentCustomer.s_waitFoodState)
            {
                if (!b_finishCooking)
                {
                    if (RestaurantManager.Instance.GetCanEmployeeCooking(out int cookingIndex) &&
                    s_currentCookingEmployee == null)
                    {
                        RestaurantManager.Instance.allEmployees[cookingIndex].s_cookingChair = this;
                        RestaurantManager.Instance.allEmployees[cookingIndex].b_canCook = false;
                        s_currentCookingEmployee = RestaurantManager.Instance.allEmployees[cookingIndex];
                    }
                }
                else
                {
                    if (RestaurantManager.Instance.GetCanEmployeeServe(out int index) &&
                                        s_currentServerEmployee == null)
                    {
                        RestaurantManager.Instance.allEmployees[index].s_serveChair = this;
                        RestaurantManager.Instance.allEmployees[index].b_canServe = false;
                        s_currentServerEmployee = RestaurantManager.Instance.allEmployees[index];
                    }
                }

            }


        }


        if (s_currentCustomer == null || s_currentCustomer.s_currentState != s_currentCustomer.s_waitFoodState)
        {
            s_currentServerEmployee = null;
            if (s_currentCookingEmployee != null)
            {
                s_currentCookingEmployee.s_cookingChair = null;
                s_currentCookingEmployee.b_canCook = true;
                s_currentCookingEmployee = null;
            }
            b_finishCooking = false;
        }

        if (!b_canUse || s_currentCustomer == null)
        {
            DisableAllFood();
        }

    }

    public void EnableAllFood()
    {
        CustomerStateManager cus = s_currentCustomer;
        if (cus != null)
        {
            switch (cus.i_dish)
            {
                case 0:
                    if (!g_beanAndBacon.activeSelf) g_beanAndBacon.SetActive(true);
                    break;
                case 1:
                    if (!g_stew.activeSelf) g_stew.SetActive(true);
                    break;
                case 2:
                    if (!g_steak.activeSelf) g_steak.SetActive(true);
                    break;
            }

            switch (cus.i_drink)
            {
                case 0:
                    if (!g_glass.activeSelf) g_glass.SetActive(true);
                    break;
                case 1:
                    if (!g_beerGlass.activeSelf) g_beerGlass.SetActive(true);
                    break;
            }
        }

    }

    public void DisableAllFood()
    {
        if (g_beanAndBacon.activeSelf) g_beanAndBacon.SetActive(false);
        if (g_beerGlass.activeSelf) g_beerGlass.SetActive(false);
        if (g_steak.activeSelf) g_steak.SetActive(false);
        if (g_glass.activeSelf) g_glass.SetActive(false);
        if (g_stew.activeSelf) g_stew.SetActive(false);
    }

    public void RandomCDForNextCustomer()
    {
        f_cdForNextCustomer = UnityEngine.Random.Range(v_minmaxCDForNextCustomer.x, v_minmaxCDForNextCustomer.y);
    }
}
