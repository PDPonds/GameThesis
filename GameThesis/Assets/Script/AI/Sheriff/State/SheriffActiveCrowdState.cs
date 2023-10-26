using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffActiveCrowdState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        SheriffStateManager sheriffStateManager = (SheriffStateManager)ai;
    }

    public override void UpdateState(StateManager ai)
    {
        SheriffStateManager sheriffStateManager = (SheriffStateManager)ai;
        sheriffStateManager.agent.SetDestination(PlayerManager.Instance.transform.position);
        sheriffStateManager.agent.speed = sheriffStateManager.f_runSpeed;

        sheriffStateManager.anim.SetBool("walk", false);
        sheriffStateManager.anim.SetBool("run", true);


        if (Vector3.Distance(sheriffStateManager.transform.position,
            PlayerManager.Instance.transform.position) <= 1f)
        {
            sheriffStateManager.agent.velocity = Vector3.zero;
            sheriffStateManager.anim.SetBool("walk", false);
            sheriffStateManager.anim.SetBool("run", false);
            sheriffStateManager.anim.Play("Angry");
            if(sheriffStateManager.anim.GetCurrentAnimatorStateInfo(0).IsName("Angry"))
            {
                if(sheriffStateManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
                {
                    GameManager.Instance.RemoveCoin(sheriffStateManager.f_cost);
                    sheriffStateManager.SwitchState(sheriffStateManager.s_activityState);
                }    
            }    

        }
    }
}
