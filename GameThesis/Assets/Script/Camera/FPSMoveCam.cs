using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMoveCam : MonoBehaviour
{
    private void Update()
    {
        transform.position = PlayerManager.Instance.t_cameraPosition.position;
    }
}
