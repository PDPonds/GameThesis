using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseRestaurantInterac : MonoBehaviour, IInteracable
{

    public void Interaction()
    {
        GameManager.Instance.s_gameState.OpenCloseRestaurant();

    }

    public string InteractionText()
    {
        string text = string.Empty;
        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_openState)
        {
            text += "[E] to Close Restaurant";
        }
        else if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_closeState)
        {
            text += "[E] to Open Restaurant";
        }
        return text;
    }
}
