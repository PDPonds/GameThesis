using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjective : MonoBehaviour
{
    public int targetDay;

    public int objectiveCost;

    public bool objectiveStatus;

    int count;
    private void Update()
    {
        if (GameManager.Instance.f_pocketMoney < 0)
        {
            UIManager.Instance.losePage.SetActive(true);
        }
        else if (GameManager.Instance.i_currentDay == targetDay)
        {
            if (!objectiveStatus)
            {
                UIManager.Instance.winPage.SetActive(false);
                UIManager.Instance.losePage.SetActive(true);
            }
        }

        if (UIManager.Instance.winPage.activeSelf || UIManager.Instance.losePage.activeSelf)
        {
            PlayerManager.Instance.b_canMove = false;
        }


    }

}
