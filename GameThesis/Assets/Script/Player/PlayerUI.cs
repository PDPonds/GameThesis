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
                    }
                    else text_interactText.text = string.Empty;
                }
                else
                {
                    IInteracable interactive = PlayerManager.Instance.g_interactiveObj.GetComponent<IInteracable>();
                    if (interactive != null)
                    {
                        text_interactText.text = interactive.InteractionText();
                    }
                    else text_interactText.text = string.Empty;
                }

            }
            else text_interactText.text = string.Empty;
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
