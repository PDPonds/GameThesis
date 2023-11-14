using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockNewMenu : MonoBehaviour, IInteracable
{
    public enum menuType { dish, drink }

    public menuType type = menuType.dish;
    public int menuNum;

    public float menuCost;
    public MenuHandler menuHandler;

    public TextMeshProUGUI text_menuName;
    public TextMeshProUGUI text_menuCost;
    public TextMeshProUGUI text_menuDollar;

    private void Update()
    {
        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (type == menuType.dish)
            {
                if (menuHandler.mainDish_Status[menuNum].status)
                {
                    text_menuCost.color = Color.white;
                    text_menuName.color = Color.white;
                    text_menuDollar.color = Color.white;
                }
                else
                {
                    text_menuCost.color = Color.gray;
                    text_menuName.color = Color.gray;
                    text_menuDollar.color = Color.gray;
                }
            }
            else if (type == menuType.drink)
            {
                if (menuHandler.drinks_Status[menuNum].status)
                {
                    text_menuCost.color = Color.white;
                    text_menuName.color = Color.white;
                    text_menuDollar.color = Color.white;
                }
                else
                {
                    text_menuCost.color = Color.gray;
                    text_menuName.color = Color.gray;
                    text_menuDollar.color = Color.gray;
                }
            }

        }

    }

    public void Interaction()
    {
        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (GameManager.Instance.f_pocketMoney >= menuCost)
            {
                if (type == menuType.dish)
                {
                    if (!menuHandler.mainDish_Status[menuNum].status)
                    {
                        menuHandler.ActivateDishMenu(menuNum);
                    }
                }
                else if (type == menuType.drink)
                {
                    if (!menuHandler.drinks_Status[menuNum].status)
                    {
                        menuHandler.ActivateDrinksMenu(menuNum);
                    }
                }
            }
        }

    }

    public string InteractionText()
    {
        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (type == menuType.dish)
            {
                if (menuHandler.mainDish_Status[menuNum].status)
                {
                    return string.Empty;
                }
                else
                {
                    return $"[E] ${menuCost} to unlock menu";
                }
            }
            else
            {
                if (menuHandler.drinks_Status[menuNum].status)
                {
                    return string.Empty;
                }
                else
                {
                    return $"[E] ${menuCost} to unlock menu";
                }
            }
        }
        else
        {
            return string.Empty;
        }

    }

    public Color InteractionTextColor()
    {
        if (GameManager.Instance.f_pocketMoney >= menuCost)
        {
            return Color.white;
        }
        else
        {
            return Color.gray;
        }
    }
}
