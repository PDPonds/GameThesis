using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseRestaurantInterac : MonoBehaviour, IInteracable
{
    private int openCounter = 0;
    public TutorialsManager tutorialManager;

    public void Interaction()
    {
        GameManager.Instance.s_gameState.OpenCloseRestaurant();

        openCounter++;
        if(openCounter ==1)
        {
            Debug.Log("Obj Complete sdsdsdasdasd");
            //tutorialManager.SetObjectiveToComplete();
        }
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
