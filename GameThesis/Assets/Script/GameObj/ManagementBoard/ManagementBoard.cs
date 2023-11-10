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

    private void Update()
    {
        text_waiterCount.text = $"{RestaurantManager.Instance.i_currentWaiterCount.ToString("0")}";
        text_cookerCount.text = $"{RestaurantManager.Instance.i_currentCookerCount.ToString("0")}";

        text_waiterCost.text = $"{RestaurantManager.Instance.f_waiterCost.ToString("00")}";
        text_cookerCost.text = $"{RestaurantManager.Instance.f_cookerCost.ToString("00")}";

        text_totalCost.text = $"{RestaurantManager.Instance.f_currentCostPerDay}";
    }

}
