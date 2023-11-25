using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseRestaurantInterac : MonoBehaviour, IInteracable
{
    public GameObject g_forntDoor;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Door forntDoor = g_forntDoor.GetComponent<Door>();
        anim.SetBool("isLock", forntDoor.b_isLock);
    }

    public void Interaction()
    {
        Door forntDoor = g_forntDoor.GetComponent<Door>();
        GameState state = GameManager.Instance.s_gameState;

        if (state.s_currentState == state.s_beforeOpenState)
        {
            if (TutorialManager.Instance.currentTutorialIndex == 5 ||
                TutorialManager.Instance.currentTutorialIndex == 25 ||
                TutorialManager.Instance.currentTutorialIndex >= 40)
            {
                if (TutorialManager.Instance.currentTutorialIndex == 5)
                {
                    TutorialManager.Instance.currentTutorialIndex = 6;
                }

                if (TutorialManager.Instance.currentTutorialIndex == 25)
                {
                    TutorialManager.Instance.currentTutorialIndex = 26;
                }

                forntDoor.b_isLock = false;
                RestaurantManager.Instance.SpawnEmp();
                SoundManager.Instance.PlayOpenRestaurantSound();
                state.SwitchState(state.s_openState);
            }

        }
        else if (state.s_currentState == state.s_afterOpenState)
        {
            if (RestaurantManager.Instance.RestaurantIsEmpty())
            {
                forntDoor.b_isLock = true;
                SoundManager.Instance.PlaySummarySound();
                SoundManager.Instance.PlayCloseRestaurantSound();
                RestaurantManager.Instance.CloseRestaurant();
            }
        }
    }

    public string InteractionText()
    {
        string text = string.Empty;
        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            if (TutorialManager.Instance.currentTutorialIndex == 5 ||
                TutorialManager.Instance.currentTutorialIndex == 25 ||
                TutorialManager.Instance.currentTutorialIndex >= 40)
            {
                text += "[E] to Open Restaurant";
            }
        }
        else if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_afterOpenState)
        {
            if (RestaurantManager.Instance.RestaurantIsEmpty())
            {
                text += "[E] to Close Restaurant";
            }
        }
        return text;
    }

    public Color InteractionTextColor()
    {
        return Color.white;
    }
}
