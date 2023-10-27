using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float f_xRotation;
    float f_yRotation;

    private void Update()
    {
        transform.localPosition = Vector3.zero;

        float f_mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * CameraController.Instance.f_senX;
        float f_mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * CameraController.Instance.f_senY;

        f_yRotation += f_mouseX;

        f_xRotation -= f_mouseY;

        if(PlayerManager.Instance.g_dragObj == null)
        {
        f_xRotation = Mathf.Clamp(f_xRotation, -90f, 90f);
        }
        else
        {
            f_xRotation = Mathf.Clamp(f_xRotation, 30, 60 );
        }

        transform.rotation = Quaternion.Euler(f_xRotation, f_yRotation, 0);
        PlayerManager.Instance.t_orientation.rotation = Quaternion.Euler(0, f_yRotation, 0);
        PlayerManager.Instance.t_playerMesh.rotation = Quaternion.Euler(f_xRotation, f_yRotation, 0);
    }

}
