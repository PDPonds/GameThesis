using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit,
            PlayerManager.Instance.f_interacRange, PlayerManager.Instance.lm_interacMask))
        {
            Debug.DrawRay(Camera.main.transform.position, hit.point - Camera.main.transform.position, Color.red);
            PlayerManager.Instance.g_interactiveObj = hit.transform.gameObject;
        }
        else
        {
            PlayerManager.Instance.g_interactiveObj = null;
        }

    }

}
