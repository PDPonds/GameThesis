using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEmp : MonoBehaviour, IInteracable
{
    public int levelTarget;
    public int cost;

    public float holdTime;
    public float currenthold;

    private void Update()
    {
        if (GameManager.Instance.f_pocketMoney >= cost &&
            RestaurantManager.Instance.i_empLevel + 1 == levelTarget &&
            PlayerManager.Instance.g_interactiveObj == this.gameObject)
        {
            if (Input.GetKey(KeyCode.E))
            {
                currenthold += Time.deltaTime;
                if (currenthold > holdTime)
                {
                    SoundManager.Instance.PlayRemoveCoinSound();
                    GameManager.Instance.RemovePocketMoney(cost);
                    RestaurantManager.Instance.i_empLevel = levelTarget;
                    currenthold = 0;
                }
            }
            else
            {
                currenthold = 0;
            }
        }
        else
        {
            currenthold = 0;
        }
    }

    public void Interaction()
    {
        //if (GameManager.Instance.f_pocketMoney >= cost &&
        //    RestaurantManager.Instance.i_empLevel + 1 == levelTarget)
        //{
        //    SoundManager.Instance.PlayRemoveCoinSound();
        //    GameManager.Instance.RemovePocketMoney(cost);
        //    RestaurantManager.Instance.i_empLevel = levelTarget;
        //}
    }

    public string InteractionText()
    {
        return $"[E] ${cost} to upgrade your employees";
    }

    public Color InteractionTextColor()
    {
        if (GameManager.Instance.f_pocketMoney >= cost &&
            RestaurantManager.Instance.i_empLevel < levelTarget)
        {
            return Color.white;
        }
        else
        {
            return Color.gray;
        }
    }
}
