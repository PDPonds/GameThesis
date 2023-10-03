using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        if(PlayerManager.Instance.b_canMove)
        {
            Vector3 v_dir = new Vector3(PlayerManager.Instance.v_moveInput.x, 0, PlayerManager.Instance.v_moveInput.y);
            v_dir.Normalize();

            v_moveDir = v_dir.z * PlayerManager.Instance.t_orientation.forward +
                v_dir.x * PlayerManager.Instance.t_orientation.right;

            if (v_dir.magnitude > 0)
            {
                transform.Translate(v_moveDir * PlayerManager.Instance.f_moveSpeed * Time.deltaTime, Space.World);
            }
        }

    }
}
