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
    public GameObject g_border;

    public float holdTime;
    public float currenthold;

    public Color color;

    private void Update()
    {
        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (GameManager.Instance.f_pocketMoney >= menuCost &&
                PlayerManager.Instance.g_interactiveObj == this.gameObject &&
                GameManager.Instance.i_currentDay > 1 &&
                !hasMenu())
            {
                if (Input.GetKey(KeyCode.E))
                {
                    currenthold += Time.deltaTime;
                    if (currenthold > holdTime)
                    {
                        if (type == menuType.dish)
                        {
                            if (!menuHandler.mainDish_Status[menuNum].status)
                            {
                                GameManager.Instance.RemovePocketMoney(menuCost);
                                SoundManager.Instance.PlayInteractiveSound();
                                menuHandler.ActivateDishMenu(menuNum);
                            }
                        }
                        else if (type == menuType.drink)
                        {
                            if (!menuHandler.drinks_Status[menuNum].status)
                            {
                                GameManager.Instance.RemovePocketMoney(menuCost);
                                SoundManager.Instance.PlayInteractiveSound();
                                menuHandler.ActivateDrinksMenu(menuNum);
                                if (menuNum == 1 && TutorialManager.Instance.currentTutorialIndex == 21)
                                {
                                    TutorialManager.Instance.currentTutorialIndex = 22;
                                }
                            }
                        }
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

            if (type == menuType.dish)
            {
                if (menuHandler.mainDish_Status[menuNum].status)
                {
                    text_menuCost.color = color;
                    text_menuName.color = color;
                    text_menuDollar.color = color;
                    if (g_border != null) g_border.SetActive(false);
                }
                else
                {
                    text_menuCost.color = Color.gray;
                    text_menuName.color = Color.gray;
                    text_menuDollar.color = Color.gray;
                    if (g_border != null) g_border.SetActive(true);
                }
            }
            else if (type == menuType.drink)
            {
                if (menuHandler.drinks_Status[menuNum].status)
                {
                    text_menuCost.color = color;
                    text_menuName.color = color;
                    text_menuDollar.color = color;
                    if (g_border != null) g_border.SetActive(false);
                }
                else
                {
                    text_menuCost.color = Color.gray;
                    text_menuName.color = Color.gray;
                    text_menuDollar.color = Color.gray;
                    if (g_border != null) g_border.SetActive(true);
                }
            }

        }

    }

    bool hasMenu()
    {
        if (type == menuType.drink)
        {
            if (menuHandler.drinks_Status[menuNum].status)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        else
        {
            if (menuHandler.mainDish_Status[menuNum].status)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
    }

    public void Interaction()
    {
        //if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState)
        //{
        //    if (GameManager.Instance.f_pocketMoney >= menuCost)
        //    {
        //        if (type == menuType.dish)
        //        {
        //            if (!menuHandler.mainDish_Status[menuNum].status)
        //            {
        //                GameManager.Instance.RemovePocketMoney(menuCost);
        //                SoundManager.Instance.PlayInteractiveSound();
        //                menuHandler.ActivateDishMenu(menuNum);
        //            }
        //        }
        //        else if (type == menuType.drink)
        //        {
        //            if (!menuHandler.drinks_Status[menuNum].status)
        //            {
        //                GameManager.Instance.RemovePocketMoney(menuCost);
        //                SoundManager.Instance.PlayInteractiveSound();
        //                menuHandler.ActivateDrinksMenu(menuNum);
        //            }
        //        }
        //    }

        //}

    }

    public string InteractionText()
    {
        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState &&
                GameManager.Instance.i_currentDay > 1)
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
