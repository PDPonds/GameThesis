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
    public GameObject g_checker;

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
        }
        else
        {
            g_addCookerButton.SetActive(false);
            g_addWaiterButton.SetActive(false);
            g_removeCookerButton.SetActive(false);
            g_removeWaiterButton.SetActive(false);
            g_upgradeRestaurant.SetActive(false);
        }

        if(RestaurantManager.Instance.i_level > 1)
        {
            g_checker.SetActive(true);
        }
        else
        {
            g_checker.SetActive(false);
        }

    }

}
