using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDragState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        PlayerManager.Instance.g_dragObj = ai.gameObject;
    }

    public override void UpdateState(StateManager ai)
    {
        Rigidbody connectRb = PlayerManager.Instance.t_dragPos.GetComponent<Rigidbody>();
        SpringJoint spring = PlayerManager.Instance.g_dragObj.GetComponentInChildren<SpringJoint>();
        spring.connectedBody = connectRb;
        ai.transform.position = PlayerManager.Instance.t_dragPos.position;
    }
}
