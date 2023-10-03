using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPunchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerManager player))
        {
            player.TakeDamage();
            if (AIController.Instance.i_atkCount % 2 == 0) AIController.Instance.c_leftHand.enabled = false;
            else AIController.Instance.c_rightHand.enabled = false;
            AIController.Instance.f_currentAtk = AIController.Instance.f_atkDelay;
            AIController.Instance.b_atk = false;
        }
    }
}
