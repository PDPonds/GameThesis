using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvent : MonoBehaviour
{
    CustomerStateManager customerStateManager;
    private void Awake()
    {
        customerStateManager = GetComponent<CustomerStateManager>();
    }
    public void EnableColliderLeftHand()
    {
        customerStateManager.c_leftHandPunch.enabled = true;
    }

    public void DisableColliderLeftHand()
    {
        customerStateManager.c_leftHandPunch.enabled = false;
    }

    public void EnableColliderRightHand()
    {
        customerStateManager.c_rightHandPunch.enabled = true;
    }

    public void DisableColliderRightHand()
    {
        customerStateManager.c_rightHandPunch.enabled = false;
    }
}
