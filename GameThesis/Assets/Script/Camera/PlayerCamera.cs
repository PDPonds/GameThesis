using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float f_xRotation;
    float f_yRotation;

    private void Update()
    {

        float f_mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * CameraController.Instance.f_senX;
        float f_mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * CameraController.Instance.f_senY;

        f_yRotation += f_mouseX;
        f_xRotation -= f_mouseY;

        if (PlayerManager.Instance.g_dragObj == null)
        {
            f_xRotation = Mathf.Clamp(f_xRotation, -90f, 90f);
        }
        else
        {
            f_xRotation = Mathf.Clamp(f_xRotation, 30, 60);
        }

        transform.rotation = Quaternion.Euler(f_xRotation, f_yRotation, 0);
        PlayerManager.Instance.t_orientation.rotation = Quaternion.Euler(0, f_yRotation, 0);
        PlayerManager.Instance.t_playerMesh.rotation = Quaternion.Euler(f_xRotation, f_yRotation, 0);

        if (PlayerManager.Instance.b_inFighting)
        {
            Vector3 playerPos = PlayerManager.Instance.transform.position;
            if (FightingManager.Instance.GetCurrentFightWithPlayer(out CustomerStateManager cus))
            {
                if (Vector3.Distance(playerPos, cus.transform.position) < PlayerManager.Instance.f_fightingCheckDis)
                {
                    Vector3 dir = cus.transform.position - playerPos;
                    dir = dir.normalized;
                    Quaternion lookat = Quaternion.LookRotation(dir);
                    Quaternion rot = Quaternion.Slerp(transform.rotation, lookat, PlayerManager.Instance.f_targetSmoothRot * Time.deltaTime);

                    rot.x = 0;
                    rot.z = 0;

                    if (f_mouseX == 0 && f_mouseY == 0)
                    {
                        transform.rotation = rot;
                        PlayerManager.Instance.t_orientation.rotation = rot;
                        PlayerManager.Instance.t_playerMesh.rotation = rot;

                        f_yRotation = transform.rotation.eulerAngles.y;
                        f_xRotation = transform.rotation.eulerAngles.x;

                    }

                }

                //float dot = Vector3.Dot(transform.forward, cus.transform.forward);
                //if (dot > 0)
                //{
                //    f_yRotation = transform.rotation.eulerAngles.y;
                //    f_xRotation = transform.rotation.eulerAngles.x;

                //    if (PlayerManager.Instance.g_dragObj == null)
                //    {
                //        f_xRotation = Mathf.Clamp(f_xRotation, -90f, 90f);
                //    }
                //    else
                //    {
                //        f_xRotation = Mathf.Clamp(f_xRotation, 30, 60);
                //    }

                //    transform.rotation = Quaternion.Euler(f_xRotation, f_yRotation, 0);
                //    PlayerManager.Instance.t_orientation.rotation = Quaternion.Euler(0, f_yRotation, 0);
                //    PlayerManager.Instance.t_playerMesh.rotation = Quaternion.Euler(f_xRotation, f_yRotation, 0);

                //}
            }

            
        }
    }

}
