using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

            ia_action.Action.Sprint.performed += i => SprintPerformed();
            ia_action.Action.Sprint.canceled += i => PlayerManager.Instance.b_isSprint = false;

            ia_action.Action.Pause.performed += i => Pause.isPause = !Pause.isPause;

            ia_action.Enable();
        }
    }


    private void OnDisable()
    {
        ia_action.Disable();
    }

    void InteractivePerformed()
    {
        if (!PlayerManager.Instance.b_isDead)
        {
            if (PlayerManager.Instance.g_dragObj != null)
            {
                if (PlayerManager.Instance.g_dragObj.TryGetComponent(out StateManager state))
                {
                    if (state is CustomerStateManager)
                    {
                        CustomerStateManager customerStateManager = (CustomerStateManager)state;
                        Destroy(customerStateManager.t_hips.GetComponent<SpringJoint>());
                        PlayerManager.Instance.g_dragObj = null;
                    }

                }
            }
            else
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
                    else
                    {
                        IInteracable interactive = PlayerManager.Instance.g_interactiveObj.GetComponent<IInteracable>();
                        if (interactive != null)
                        {
                            interactive.Interaction();
                        }
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
                PlayerManager.Instance.b_inFighting = true;
                PlayerManager.Instance.f_currentInFightingTime = PlayerManager.Instance.f_maxInFightingTime;
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
                PlayerManager.Instance.b_inFighting = true;
                PlayerManager.Instance.f_currentInFightingTime = PlayerManager.Instance.f_maxInFightingTime;
            }
        }

    }

    void GuardCanceled()
    {
        PlayerManager.Instance.b_canGuard = false;
        PlayerManager.Instance.b_isGuard = false;
    }

    void SprintPerformed()
    {
        if (!PlayerManager.Instance.b_isDead)
        {
            PlayerManager.Instance.b_isSprint = true;
        }
    }

}
