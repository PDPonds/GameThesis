using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseRestaurantInterac : MonoBehaviour, IInteracable
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isOpen = GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_openState;
        anim.SetBool("isOpen", isOpen);
    }

    public void Interaction()
    {
        GameManager.Instance.s_gameState.OpenCloseRestaurant();
    }

    public string InteractionText()
    {
        string text = string.Empty;
        if (GameManager.Instance.s_gameState.s_currentState == GameManager.Instance.s_gameState.s_beforeOpenState)
        {
            text += "[E] to Open Restaurant";
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
}
