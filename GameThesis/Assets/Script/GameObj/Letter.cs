using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IInteracable
{
    public float holdTime;
    float currentHoldTime;

    public Image fillR;

    private void Update()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, 2.5f, GameManager.Instance.lm_playerMask);
        if (player.Length <= 0)
        {
            if (UIManager.Instance.letter.activeSelf)
            {
                UIManager.Instance.letter.SetActive(false);
            }
        }

        if(UIManager.Instance.letter.activeSelf)
        {

            if (Input.GetKey(KeyCode.R))
            {
                if (!ObjectiveManager.Instance.mainObjtive.objectiveStatus &&
                    GameManager.Instance.f_pocketMoney >= ObjectiveManager.Instance.mainObjtive.objectiveCost)
                {
                    currentHoldTime += Time.deltaTime;
                    if (currentHoldTime >= holdTime)
                    {
                        ObjectiveManager.Instance.mainObjtive.objectiveStatus = true;
                        UIManager.Instance.winPage.SetActive(true);
                        UIManager.Instance.letter.SetActive(false);
                    }
                }
            }
            else
            {
                currentHoldTime = 0;
            }
        }

        float percent = currentHoldTime / holdTime;
        fillR.fillAmount = percent;

    }

    public void Interaction()
    {
        UIManager.Instance.letter.SetActive(true);
    }

    public string InteractionText()
    {
        return "[E] to read";
    }

    public Color InteractionTextColor()
    {
        return Color.white;
    }
}
