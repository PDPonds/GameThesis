using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void Update()
    {
        if (PlayerManager.Instance.g_dragObj == null)
        {
            if (PlayerManager.Instance.g_interactiveObj != null)
            {

                if (PlayerManager.Instance.g_interactiveObj.transform.parent != null)
                {
                    IInteracable interactive = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<IInteracable>();
                    if (interactive != null)
                    {
                        text_interactText.text = interactive.InteractionText();
                        text_interactText.color = interactive.InteractionTextColor();
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
                        g_wekeUp.SetActive(false);
                        text_interactText.text = string.Empty;
                    }
                }
                else
                {
                    IInteracable interactive = PlayerManager.Instance.g_interactiveObj.GetComponent<IInteracable>();
                    if (interactive != null)
                    {
                        text_interactText.text = interactive.InteractionText();
                        text_interactText.color = interactive.InteractionTextColor();
                    }
                    else text_interactText.text = string.Empty;
                }

            }
            else
            {
                g_wekeUp.SetActive(false);
                text_interactText.text = string.Empty;
            }
        }
        else
        {
            text_interactText.text = "[E] to Drop";
        }

        float staminaPercent = PlayerManager.Instance.f_currentStamina / PlayerManager.Instance.f_maxStamina;
        s_staminaSlider.value = staminaPercent;

        float hpPercent = ((float)PlayerManager.Instance.i_maxHP - (float)PlayerManager.Instance.i_currentHP)
            / PlayerManager.Instance.i_maxHP;
        s_hpSlider.value = hpPercent;

    }
}
