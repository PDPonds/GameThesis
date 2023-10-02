using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Auto_Singleton<CameraController>
{
    [HideInInspector] public PlayerCamera s_playerCamera;
    [HideInInspector] public FPSMoveCam s_fpsMoveCam;
    [Header("===== PlayerCam =====")]
    public float f_senX;
    public float f_senY;

    private void Awake()
    {
        s_playerCamera = Camera.main.transform.GetComponent<PlayerCamera>();
        s_fpsMoveCam = GetComponent<FPSMoveCam>();
    }

}
