using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGiveMoneyBackState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        Color noColor = new Color(0, 0, 0, 0);
        cus.ApplyOutlineColor(noColor, 0f);

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

        for (int i = 0; i < RestaurantManager.Instance.allSheriffs.Length; i++)
        {
            SheriffStateManager shrSM = RestaurantManager.Instance.allSheriffs[i];
            if (shrSM.s_currentState == shrSM.s_waitForFightEnd)
            {
                shrSM.SwitchState(shrSM.s_activityState);
            }
        }

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        cus.transform.LookAt(PlayerManager.Instance.transform.position);
        cus.agent.velocity = Vector3.zero;
        cus.anim.Play("Searching");
        if (cus.anim.GetCurrentAnimatorStateInfo(0).IsName("Searching"))
        {
            if (cus.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                GameManager.Instance.AddCoin(cus.f_giveCoin);
                cus.SwitchState(cus.s_runEscapeState);
            }
        }
    }
}
