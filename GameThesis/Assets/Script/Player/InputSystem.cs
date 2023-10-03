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

            ia_action.Action.Crouch.performed += i => PlayerManager.Instance.b_isCrouch = true;
            ia_action.Action.Crouch.canceled += i => PlayerManager.Instance.b_isCrouch = false;

            ia_action.Action.HoldPunch.performed += i => PlayerManager.Instance.s_playerFistCombat.holdButton();
            ia_action.Action.HoldPunch.canceled += i => PlayerManager.Instance.s_playerFistCombat.releaseHoldButton();

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
        if (PlayerManager.Instance.g_interactiveObj != null)
        {
            if (PlayerManager.Instance.g_interactiveObj.transform.parent != null)
            {
                IInteracable interactive = PlayerManager.Instance.g_interactiveObj.GetComponentInParent<IInteracable>();
                if(interactive != null)
                {
                    interactive.Interaction();
                }
            }
        }
    }

}
