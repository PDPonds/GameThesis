using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    PlayerInputAction ia_action;

    private void OnEnable()
    {
        ia_action = new PlayerInputAction();
        if (ia_action != null)
        {
            ia_action.PlayerMovement.Move.performed += i => PlayerManager.Instance.v_moveInput = i.ReadValue<Vector2>();

            ia_action.Action.HoldPunch.performed += i => HoldPerformed();
            ia_action.Action.HoldPunch.canceled += i => PlayerManager.Instance.b_isHold = false;

            ia_action.Action.Guard.performed += i => GuardPerformed();
            ia_action.Action.Guard.canceled += i => GuardCanceled();

            ia_action.Action.Interactive.performed += i => InteractivePerformed();

            ia_action.Enable();
        }
    }


    private void OnDisable()
    {
        ia_action.Disable();
    }

    void InteractivePerformed()
    {
        if(!PlayerManager.Instance.b_isDead)
        {
            if (PlayerManager.Instance.g_interactiveObj != null)
            {
                if (PlayerManager.Instance.g_interactiveObj.transform.parent != null)
                {
                    IInteracable interactive = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<IInteracable>();
                    if (interactive != null)
                    {
                        interactive.Interaction();
                    }
                }
            }
        }
        
    }

    void HoldPerformed()
    {
        if (!PlayerManager.Instance.b_isDead)
        {
            if (PlayerManager.Instance.b_canPunch)
            {
                PlayerManager.Instance.b_isHold = true;
            }
        }
            
    }

    void GuardPerformed()
    {
        if (!PlayerManager.Instance.b_isDead)
        {
            if (PlayerManager.Instance.b_canGuard)
            {
                PlayerManager.Instance.b_isGuard = true;
            }
        }
        
    }

    void GuardCanceled()
    {
        PlayerManager.Instance.b_canGuard = false;
        PlayerManager.Instance.b_isGuard = false;
    }
}
