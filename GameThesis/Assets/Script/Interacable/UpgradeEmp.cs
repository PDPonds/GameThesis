using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEmp : MonoBehaviour, IInteracable
{
    public int levelTarget;
    public int cost;

    public void Interaction()
    {
        if (GameManager.Instance.f_pocketMoney >= cost &&
            RestaurantManager.Instance.i_empLevel < levelTarget)
        {
            SoundManager.Instance.PlayRemoveCoinSound();
            GameManager.Instance.RemovePocketMoney(cost);
            RestaurantManager.Instance.i_empLevel = levelTarget;
        }
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
