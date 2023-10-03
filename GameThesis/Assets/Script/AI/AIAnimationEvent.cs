using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvent : MonoBehaviour
{
    public void EnableColliderLeftHand()
    {
        AIController.Instance.c_leftHand.enabled = true;
    }

    public void DisableColliderLeftHand()
    {
        AIController.Instance.c_leftHand.enabled = false;
    }

    public void EnableColliderRightHand()
    {
        AIController.Instance.c_rightHand.enabled = true;
    }

    public void DisableColliderRightHand()
    {
        AIController.Instance.c_rightHand.enabled = false;
    }
}
