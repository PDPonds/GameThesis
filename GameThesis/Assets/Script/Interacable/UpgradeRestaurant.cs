using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeRestaurant : MonoBehaviour, IInteracable
{
    public float f_cost;

    public TextMeshProUGUI text;

    private void Update()
    {
        if (GameManager.Instance.f_pocketMoney >= f_cost)
        {
            text.color = Color.white;
        }
        else
        {
            text.color = Color.gray;
        }
    }

    public void Interaction()
    {
        if (GameManager.Instance.f_pocketMoney >= f_cost)
        {
            SoundManager.Instance.PlayInteractiveSound();
            GameManager.Instance.RemovePocketMoney(f_cost);
            RestaurantManager.Instance.i_level = 2;
        }
    }

    public string InteractionText()
    {
        return $"[E] ${f_cost} to upgrade restaurant.";
    }

    public Color InteractionTextColor()
    {
        if (GameManager.Instance.f_pocketMoney >= f_cost)
        {
            return Color.white;
        }
        else
        {
            return Color.gray;
        }
    }
}
