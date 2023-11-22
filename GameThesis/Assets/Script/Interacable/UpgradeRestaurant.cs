using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeRestaurant : MonoBehaviour, IInteracable
{
    public float f_cost;

    public TextMeshProUGUI text;

    public float holdTime;
    public float currenthold;

    private void Update()
    {
        if (GameManager.Instance.f_pocketMoney >= f_cost &&
            RestaurantManager.Instance.i_level < 2 &&
            PlayerManager.Instance.g_interactiveObj == this.gameObject &&
            GameManager.Instance.i_currentDay > 2)
        {
            if (Input.GetKey(KeyCode.E))
            {
                currenthold += Time.deltaTime;
                if (currenthold >= holdTime)
                {
                    SoundManager.Instance.PlayInteractiveSound();
                    GameManager.Instance.RemovePocketMoney(f_cost);
                    RestaurantManager.Instance.i_level = 2;
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

        //if (GameManager.Instance.f_pocketMoney >= f_cost)
        //{
        //    SoundManager.Instance.PlayInteractiveSound();
        //    GameManager.Instance.RemovePocketMoney(f_cost);
        //    RestaurantManager.Instance.i_level = 2;
        //}
    }

    public string InteractionText()
    {
        if (GameManager.Instance.i_currentDay > 2)
        {
            return $"[E] ${f_cost} to upgrade restaurant.";
        }
        return string.Empty;
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
