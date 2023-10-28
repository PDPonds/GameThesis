using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MainObserver
{
    Vector3 v_moveDir;

    private void Start()
    {
        PlayerManager.Instance.c_rb.freezeRotation = true;
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (PlayerManager.Instance.b_canMove)
        {
            Vector3 v_dir = new Vector3(PlayerManager.Instance.v_moveInput.x, 0, PlayerManager.Instance.v_moveInput.y);

            Vector3 forward = v_dir.z * PlayerManager.Instance.t_orientation.forward;
            Vector3 right = v_dir.x * PlayerManager.Instance.t_orientation.right;
            forward = forward.normalized;
            right = right.normalized;
            v_moveDir = forward + right;
            v_moveDir.Normalize();

            if (v_dir.magnitude > 0)
            {
                PlayerManager.Instance.c_rb.velocity = new Vector3(v_moveDir.x * PlayerManager.Instance.f_moveSpeed,
                PlayerManager.Instance.c_rb.velocity.y, v_moveDir.z * PlayerManager.Instance.f_moveSpeed);
            }

            if (v_dir != Vector3.zero)
            {
                if (PlayerManager.Instance.currentAreaStay == AreaType.InRestaurant)
                    ActiveAllObserver(ActionObserver.PlayerWalkInRestaurant);
                if (PlayerManager.Instance.currentAreaStay == AreaType.OutRestaurant)
                    ActiveAllObserver(ActionObserver.PlayerWalkOutRestaurant);
            }
            else ActiveAllObserver(ActionObserver.PlayerStopWalk);
        }

    }
}
