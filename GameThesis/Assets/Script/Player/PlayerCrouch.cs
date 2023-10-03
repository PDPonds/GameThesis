using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private void Update()
    {
        if (PlayerManager.Instance.b_isCrouch)
        {
            Vector3 crouchCam = new Vector3(0, PlayerManager.Instance.f_crouchHeight, 0);
            if (PlayerManager.Instance.t_cameraPosition.transform.localPosition != crouchCam)
            {
                PlayerManager.Instance.t_cameraPosition.transform.localPosition = Vector3.Lerp(
                    PlayerManager.Instance.t_cameraPosition.transform.localPosition, crouchCam, 10f * Time.deltaTime);
            }
            if (PlayerManager.Instance.t_playerMesh.transform.localPosition != crouchCam)
            {
                PlayerManager.Instance.t_playerMesh.transform.localPosition = Vector3.Lerp(
                    PlayerManager.Instance.t_playerMesh.transform.localPosition, crouchCam, 10f * Time.deltaTime);
            }
            PlayerManager.Instance.t_orientation.transform.localPosition = crouchCam;

            Vector3 crouchCenter = new Vector3(0, PlayerManager.Instance.f_crouchHeight / 2f, 0);
            PlayerManager.Instance.c_collider.height = PlayerManager.Instance.f_crouchHeight;
            PlayerManager.Instance.c_collider.center = crouchCenter;

        }
        else
        {
            Vector3 standCam = new Vector3(0, PlayerManager.Instance.f_standHeight, 0);
            if (PlayerManager.Instance.t_cameraPosition.transform.localPosition != standCam)
            {
                PlayerManager.Instance.t_cameraPosition.transform.localPosition = Vector3.Lerp(
                    PlayerManager.Instance.t_cameraPosition.transform.localPosition, standCam, 10f * Time.deltaTime);
            }
            if (PlayerManager.Instance.t_playerMesh.transform.localPosition != standCam)
            {
                PlayerManager.Instance.t_playerMesh.transform.localPosition = Vector3.Lerp(
                    PlayerManager.Instance.t_playerMesh.transform.localPosition, standCam, 10f * Time.deltaTime);
            }
            PlayerManager.Instance.t_orientation.transform.localPosition = standCam;

            Vector3 standCenter = new Vector3(0, PlayerManager.Instance.f_standHeight / 2f, 0);
            PlayerManager.Instance.c_collider.height = PlayerManager.Instance.f_standHeight;
            PlayerManager.Instance.c_collider.center = standCenter;
        }
    }
}
