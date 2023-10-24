using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGiveMoneyBackState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        Color noColor = new Color(0, 0, 0, 0);
        customerStateManager.ApplyOutlineColor(noColor, 0f);
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.transform.LookAt(PlayerManager.Instance.transform.position);
        customerStateManager.agent.velocity = Vector3.zero;
        customerStateManager.anim.Play("Searching");
        if (customerStateManager.anim.GetCurrentAnimatorStateInfo(0).IsName("Searching"))
        {
            if (customerStateManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                GameManager.Instance.AddCoin(customerStateManager.f_giveCoin);
                customerStateManager.SwitchState(customerStateManager.s_runEscapeState);
            }
        }
    }
}
