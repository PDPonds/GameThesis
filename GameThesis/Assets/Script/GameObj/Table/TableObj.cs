using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableObj : MonoBehaviour
{
    public GameObject g_table;
    public List<GameObject> g_chairs = new List<GameObject>();
    public List<GameObject> g_foods = new List<GameObject>();

    public float f_dirtyCount;

    public bool b_isEmpty;

    [HideInInspector] public float f_cdForNextCustomer;
    public Vector2 v_minmaxCDForNextCustomer;

    [HideInInspector] public bool b_readyForNextCustomer;
    [SerializeField] float f_currentCDForNextCustomer;

    public CustomerStateManager s_currentCustomer;
    public EmployeeStateManager s_currentEmployee;

    private void Awake()
    {
        b_isEmpty = true;
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
            if (s_currentCustomer.c_tableObj != this)
            {
                s_currentCustomer = null;
                return;
            }

            if (s_currentCustomer.s_currentState == s_currentCustomer.s_waitFoodState)
            {

                if (RestaurantManager.Instance.GetCanEmployeeServe(out int index) &&
                    s_currentEmployee == null)
                {
                    RestaurantManager.Instance.allEmployees[index].s_serveTable = this;
                    RestaurantManager.Instance.allEmployees[index].b_canServe = false;
                    s_currentEmployee = RestaurantManager.Instance.allEmployees[index];
                }
            }


            if (s_currentCustomer.s_currentState == s_currentCustomer.s_eatFoodState)
            {
                foreach (GameObject food in g_foods) food.SetActive(true);

            }
            else
            {
                foreach (GameObject food in g_foods) food.SetActive(false);
            }

        }
        else
        {
            foreach (GameObject food in g_foods) food.SetActive(false);

        }

        if (s_currentCustomer == null || s_currentCustomer.s_currentState != s_currentCustomer.s_waitFoodState)
        {
            s_currentEmployee = null;
        }
    }

    public void RandomCDForNextCustomer()
    {
        f_cdForNextCustomer = Random.Range(v_minmaxCDForNextCustomer.x, v_minmaxCDForNextCustomer.y);
    }


}
