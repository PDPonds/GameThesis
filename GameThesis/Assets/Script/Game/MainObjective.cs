using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjective : MonoBehaviour
{
    public int targetDay;

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
            if (objectiveStatus)
            {
                if (count == 0)
                {
                    UIManager.Instance.winPage.SetActive(true);
                    UIManager.Instance.losePage.SetActive(false);
                    count++;
                }
            }
            else
            {
                UIManager.Instance.winPage.SetActive(false);
                UIManager.Instance.losePage.SetActive(true);
            }

        }
        else
        {
            UIManager.Instance.winPage.SetActive(false);
            UIManager.Instance.losePage.SetActive(false);
        }

        if (UIManager.Instance.winPage.activeSelf || UIManager.Instance.losePage.activeSelf)
        {
            PlayerManager.Instance.b_canMove = false;
        }

    }

}
