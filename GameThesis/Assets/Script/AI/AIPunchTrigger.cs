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

            CustomerStateManager customerStateManager = transform.GetComponentInParent<CustomerStateManager>();

            if (customerStateManager.i_atkCount % 2 == 0)
            {
                customerStateManager.c_leftHandPunch.enabled = false;
            }
            else
            {
                customerStateManager.c_rightHandPunch.enabled = false;
            }
        }
    }
}
