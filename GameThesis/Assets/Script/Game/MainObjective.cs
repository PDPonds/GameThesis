using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjective : MonoBehaviour
{
    [HideInInspector] public float currentMainCost;

    public float day1Cost;
    public float day2Cost;
    public float day3Cost;
    public float day4Cost;
    public float day5Cost;

    private void Update()
    {
        if (GameManager.Instance.f_pocketMoney < 0)
        {
            UIManager.Instance.losePage.SetActive(true);
        }

        if (UIManager.Instance.winPage.activeSelf || UIManager.Instance.losePage.activeSelf)
        {
            PlayerManager.Instance.b_canMove = false;
        }

        if (GameManager.Instance.i_currentDay < 3)
        {
            currentMainCost = 0;
        }
        else
        {
            switch (GameManager.Instance.i_currentDay)
            {
                case 3:
                    currentMainCost = day1Cost;
                    break;
                case 4:
                    currentMainCost = day2Cost;
                    break;
                case 5:
                    currentMainCost = day3Cost;
                    break;
                case 6:
                    currentMainCost = day4Cost;
                    break;
                case 7:
                    currentMainCost = day5Cost;
                    break;
            }
        }



    }

}
