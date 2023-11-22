using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : Auto_Singleton<PlayerUI>
{
    [Header("===== Interactive =====")]
    public TextMeshProUGUI text_interactText;

    [Header("===== WakeUpDrunkCustomer =====")]
    public GameObject g_wekeUp;
    public Image image_fillAmouny;

    [Header("===== Cooking =====")]
    public GameObject cookingUI;
    public Image minPoint;
    public Image maxPoint;
    public Image currentTime;
    public RectTransform currentPoint;

    [Header("===== Hold =====")]
    public Image interactiveFill;


    private void Update()
    {
        if (PlayerManager.Instance.g_dragObj == null)
        {
            if (PlayerManager.Instance.g_interactiveObj != null)
            {
                if (PlayerManager.Instance.g_interactiveObj.transform.parent != null)
                {
                    IInteracable interactiveParent = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<IInteracable>();

                    if (interactiveParent != null)
                    {
                        text_interactText.text = interactiveParent.InteractionText();
                        text_interactText.color = interactiveParent.InteractionTextColor();
                        CustomerStateManager cus = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<CustomerStateManager>();
                        if (cus != null)
                        {
                            if (cus.s_currentState == cus.s_drunkState)
                            {
                                g_wekeUp.SetActive(true);
                                float percent = cus.f_currentWekeUpPoint / cus.f_maxWekeUpPoint;
                                image_fillAmouny.fillAmount = percent;
                            }
                            else
                            {
                                g_wekeUp.SetActive(false);
                            }
                        }
                        else
                        {
                            g_wekeUp.SetActive(false);
                        }


                        UpgradeEmp upEmp = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<UpgradeEmp>();
                        UnlockNewMenu unlockMenu = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<UnlockNewMenu>();
                        UpgradeRestaurant upRes = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<UpgradeRestaurant>();
                        UpgradTable upTable = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<UpgradTable>();

                        if (upEmp != null)
                        {
                            interactiveFill.gameObject.SetActive(true);
                            float percent = upEmp.currenthold / upEmp.holdTime;
                            interactiveFill.fillAmount = percent;
                        }
                        else if (unlockMenu != null)
                        {
                            interactiveFill.gameObject.SetActive(true);
                            float percent = unlockMenu.currenthold / unlockMenu.holdTime;
                            interactiveFill.fillAmount = percent;
                        }
                        else if (upRes != null)
                        {
                            interactiveFill.gameObject.SetActive(true);
                            float percent = upRes.currenthold / upRes.holdTime;
                            interactiveFill.fillAmount = percent;
                        }
                        else if (upTable != null)
                        {
                            interactiveFill.gameObject.SetActive(true);
                            float percent = upTable.currenthold / upTable.holdTime;
                            interactiveFill.fillAmount = percent;
                        }
                        else
                        {
                            interactiveFill.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        IInteracable interactiveParent2 = PlayerManager.Instance.g_interactiveObj.GetComponent<IInteracable>();
                        IInteracable interactive2 = PlayerManager.Instance.g_interactiveObj.GetComponent<IInteracable>();
                        if (interactiveParent2 != null)
                        {
                            text_interactText.text = interactiveParent2.InteractionText();
                            text_interactText.color = interactiveParent2.InteractionTextColor();
                        }
                        else if (interactive2 != null)
                        {
                            text_interactText.text = interactive2.InteractionText();
                            text_interactText.color = interactive2.InteractionTextColor();
                        }
                        else text_interactText.text = string.Empty;
                    }

                }
                else
                {
                    IInteracable interactive = PlayerManager.Instance.g_interactiveObj.GetComponent<IInteracable>();
                    if (interactive != null)
                    {
                        text_interactText.text = interactive.InteractionText();
                        text_interactText.color = interactive.InteractionTextColor();
                        PotAndPan pot = PlayerManager.Instance.g_interactiveObj.GetComponent<PotAndPan>();
                        if (pot != null)
                        {
                            GameState state = GameManager.Instance.s_gameState;

                            if (state.s_currentState == state.s_openState)
                            {
                                if (pot.b_canUse)
                                {
                                    cookingUI.SetActive(true);

                                    minPoint.fillAmount = pot.minTargetPoint / 10f;
                                    maxPoint.fillAmount = 1f - (pot.maxTargetPoint / 10f);

                                    float pos = (pot.currentPoint / pot.maxPoint) * 250f;
                                    currentPoint.anchoredPosition = new Vector2(0, pos);

                                    currentTime.fillAmount = pot.currentTime / pot.cookingTime;
                                }
                                else
                                {
                                    cookingUI.SetActive(false);
                                    text_interactText.text = string.Empty;
                                }
                            }
                            else if (state.s_currentState == state.s_afterOpenState)
                            {

                            }
                            else
                            {
                                cookingUI.SetActive(false);
                                text_interactText.text = string.Empty;
                            }
                        }
                        else
                        {
                            cookingUI.SetActive(false);
                            text_interactText.text = string.Empty;
                        }
                    }
                    else
                    {
                        cookingUI.SetActive(false);
                        text_interactText.text = string.Empty;
                    }

                    UpgradeEmp upEmp = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<UpgradeEmp>();
                    UnlockNewMenu unlockMenu = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<UnlockNewMenu>();
                    UpgradeRestaurant upRes = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<UpgradeRestaurant>();
                    UpgradTable upTable = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<UpgradTable>();

                    if (upEmp != null)
                    {
                        interactiveFill.gameObject.SetActive(true);
                        float percent = upEmp.currenthold / upEmp.holdTime;
                        interactiveFill.fillAmount = percent;
                    }
                    else if (unlockMenu != null)
                    {
                        interactiveFill.gameObject.SetActive(true);
                        float percent = unlockMenu.currenthold / unlockMenu.holdTime;
                        interactiveFill.fillAmount = percent;
                    }
                    else if (upRes != null)
                    {
                        interactiveFill.gameObject.SetActive(true);
                        float percent = upRes.currenthold / upRes.holdTime;
                        interactiveFill.fillAmount = percent;
                    }
                    else if (upTable != null)
                    {
                        interactiveFill.gameObject.SetActive(true);
                        float percent = upTable.currenthold / upTable.holdTime;
                        interactiveFill.fillAmount = percent;
                    }
                    else
                    {
                        interactiveFill.gameObject.SetActive(false);
                    }

                }
            }
            else
            {
                interactiveFill.gameObject.SetActive(false);
                cookingUI.SetActive(false);
                g_wekeUp.SetActive(false);
                text_interactText.text = string.Empty;
            }

        }
        else
        {
            text_interactText.text = "[E] to Drop";
        }
    }
}
