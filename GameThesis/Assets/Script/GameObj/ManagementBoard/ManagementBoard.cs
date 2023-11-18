using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagementBoard : MonoBehaviour
{
    public TextMeshProUGUI text_waiterCount;
    public TextMeshProUGUI text_cookerCount;
    public TextMeshProUGUI text_waiterCost;
    public TextMeshProUGUI text_cookerCost;
    public TextMeshProUGUI text_totalCost;

    public GameObject g_addCookerButton;
    public GameObject g_removeCookerButton;
    public GameObject g_addWaiterButton;
    public GameObject g_removeWaiterButton;
    public GameObject g_upgradeRestaurant;

    [Header("===== Checker =====")]
    public GameObject g_upgradeResChecker;
    public GameObject g_upgradeResBorder;
    public GameObject g_upgradeEmp1Checker;
    public GameObject g_upgradeEmp1Border;
    public GameObject g_upgradeEmp2Checker;
    public GameObject g_upgradeEmp2Border;


    private void Update()
    {
        text_waiterCount.text = $"{RestaurantManager.Instance.i_currentWaiterCount.ToString("0")}";
        text_cookerCount.text = $"{RestaurantManager.Instance.i_currentCookerCount.ToString("0")}";

        float waiterCost = RestaurantManager.Instance.f_waiterCost * RestaurantManager.Instance.i_currentWaiterCount;
        text_waiterCost.text = $"{waiterCost}$";

        float cookerCost = RestaurantManager.Instance.f_cookerCost * RestaurantManager.Instance.i_currentCookerCount;
        text_cookerCost.text = $"{cookerCost}$";

        text_totalCost.text = $"{RestaurantManager.Instance.f_currentCostPerDay}";

        GameState state = GameManager.Instance.s_gameState;
        if (state.s_currentState == state.s_beforeOpenState)
        {
            g_addCookerButton.SetActive(true);
            g_addWaiterButton.SetActive(true);
            g_removeCookerButton.SetActive(true);
            g_removeWaiterButton.SetActive(true);
            g_upgradeRestaurant.SetActive(true);

            if (RestaurantManager.Instance.i_level == 1)
            {
                g_upgradeResBorder.SetActive(true);
            }
            else if (RestaurantManager.Instance.i_level == 2)
            {
                g_upgradeResBorder.SetActive(false);
            }

            if(RestaurantManager.Instance.i_empLevel == 1)
            {
                g_upgradeEmp1Border.SetActive(true);
                g_upgradeEmp2Border.SetActive(true);

            }
            else if (RestaurantManager.Instance.i_empLevel == 2)
            {
                g_upgradeEmp1Border.SetActive(true);
                g_upgradeEmp2Border.SetActive(false);
            }
            else if (RestaurantManager.Instance.i_empLevel == 3)
            {
                g_upgradeEmp1Border.SetActive(false);
                g_upgradeEmp2Border.SetActive(false);
            }
        }
        else
        {
            g_addCookerButton.SetActive(false);
            g_addWaiterButton.SetActive(false);
            g_removeCookerButton.SetActive(false);
            g_removeWaiterButton.SetActive(false);
            g_upgradeRestaurant.SetActive(false);
        }

        if (RestaurantManager.Instance.i_level > 1)
        {
            g_upgradeResChecker.SetActive(true);
        }
        else
        {
            g_upgradeResChecker.SetActive(false);
        }

        if (RestaurantManager.Instance.i_empLevel == 1)
        {
            g_upgradeEmp1Checker.SetActive(false);
            g_upgradeEmp2Checker.SetActive(false);

        }
        else if (RestaurantManager.Instance.i_empLevel == 2)
        {
            g_upgradeEmp1Checker.SetActive(false);
            g_upgradeEmp2Checker.SetActive(true);
        }
        else if (RestaurantManager.Instance.i_empLevel == 3)
        {
            g_upgradeEmp1Checker.SetActive(true);
            g_upgradeEmp2Checker.SetActive(true);
        }

    }

}
