using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public void EnableCollider()
    {
        PlayerManager.Instance.c_punchCol.enabled = true;

    }


    public void DisableCollider()
    {
        PlayerManager.Instance.c_punchCol.enabled = false;
        //PlayerManager.Instance.b_lockTarget = false;

    }
}
