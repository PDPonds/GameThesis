using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IInteracable
{
    //public float holdTime;
    //float currentHoldTime;

    public Image fillR;

    private void Update()
    {

        if (UIManager.Instance.letter.activeSelf)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                UIManager.Instance.letter.SetActive(false);
            }

        }


    }

    public void Interaction()
    {
        if (!UIManager.Instance.letter.activeSelf)
        {
            if (TutorialManager.Instance.currentTutorialIndex == 39)
            {
                TutorialManager.Instance.currentTutorialIndex = 40;
            }
            UIManager.Instance.letter.SetActive(true);
        }
    }

    public string InteractionText()
    {
        return $"[E] to read";

    }

    public Color InteractionTextColor()
    {
        return Color.white;
    }
}
