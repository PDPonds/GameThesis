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

    [Header("===== Stamina =====")]
    public Slider s_staminaSlider;

    [Header("===== Player HP =====")]
    public Slider s_hpSlider;

    [Header("===== WakeUpDrunkCustomer =====")]
    public GameObject g_wekeUp;
    public Image image_fillAmouny;

    [Header("===== Cooking =====")]
    public GameObject cookingUI;
    public Image minPoint;
    public Image maxPoint;
    public Image currentTime;
    public RectTransform currentPoint;


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
                        PotAndPan pot = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<PotAndPan>();
                        if (pot != null)
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
                    else
                    {
                        cookingUI.SetActive(false);
                        text_interactText.text = string.Empty;
                    }

                }
            }
            else
            {
                cookingUI.SetActive(false);
                g_wekeUp.SetActive(false);
                text_interactText.text = string.Empty;
            }

            float staminaPercent = PlayerManager.Instance.f_currentStamina / PlayerManager.Instance.f_maxStamina;
            s_staminaSlider.value = staminaPercent;

            float hpPercent = ((float)PlayerManager.Instance.i_maxHP - (float)PlayerManager.Instance.i_currentHP)
                / PlayerManager.Instance.i_maxHP;
            s_hpSlider.value = hpPercent;

        }
        else
        {
            text_interactText.text = "[E] to Drop";
        }
    }
}
