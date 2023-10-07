using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : Auto_Singleton<PlayerUI>
{
    [Header("===== Player UI =====")]
    public Image img_blood;

    [Header("===== Interactive =====")]
    public TextMeshProUGUI text_interactText;

    private void Update()
    {
        if (PlayerManager.Instance.i_currentHP < PlayerManager.Instance.i_maxHP)
        {
            img_blood.gameObject.SetActive(true);
            img_blood.color = new Color(img_blood.color.r, img_blood.color.g, img_blood.color.b, 1f / PlayerManager.Instance.i_currentHP);
        }
        else
        {
            img_blood.gameObject.SetActive(false);
        }

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
    }
}
