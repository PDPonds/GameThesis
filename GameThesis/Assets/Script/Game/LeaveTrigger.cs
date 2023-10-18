using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            UIManager.Instance.b_leaveTrigger = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            UIManager.Instance.b_leaveTrigger = false;
        }
    }
}
