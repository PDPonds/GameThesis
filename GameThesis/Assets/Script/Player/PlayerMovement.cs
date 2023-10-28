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
            Vector3 currentVelocity = PlayerManager.Instance.c_rb.velocity;
            Vector3 v_dir = new Vector3(PlayerManager.Instance.v_moveInput.x, 0, PlayerManager.Instance.v_moveInput.y);

            v_moveDir = v_dir.z * PlayerManager.Instance.t_orientation.forward +
                v_dir.x * PlayerManager.Instance.t_orientation.right;

            v_moveDir *= PlayerManager.Instance.f_moveSpeed;
            v_moveDir = transform.TransformDirection(v_moveDir);

            Vector3 velocityChange = (v_moveDir - currentVelocity);
            velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);
            Vector3.ClampMagnitude(velocityChange, 2);
            PlayerManager.Instance.c_rb.AddForce(velocityChange, ForceMode.VelocityChange);

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
