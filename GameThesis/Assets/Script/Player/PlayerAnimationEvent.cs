using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public void EnableLeftCollider()
    {
        PlayerManager.Instance.c_leftHandPunch.enabled = true;
    }

    public void EnableRightCollider()
    {
        PlayerManager.Instance.c_rightHandPunch.enabled = true;
    }

    public void DisableCollider()
    {
        PlayerManager.Instance.c_leftHandPunch.enabled = false;
        PlayerManager.Instance.c_rightHandPunch.enabled = false;
    }
}
