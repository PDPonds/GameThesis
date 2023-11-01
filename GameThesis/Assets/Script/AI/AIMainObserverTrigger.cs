using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMainObserverTrigger : MainObserver
{
    private void Update()
    {
        StateManager state = transform.GetComponent<StateManager>();
        if (state is CustomerStateManager)
        {
            CustomerStateManager cus = (CustomerStateManager)state;
            if (cus.agent.velocity != Vector3.zero) ActiveAllObserver(ActionObserver.AIWalk);
            else ActiveAllObserver(ActionObserver.AIExitWalk);

            if (cus.s_currentState == cus.s_eatFoodState) ActiveAllObserver(ActionObserver.AIEat);
            else ActiveAllObserver(ActionObserver.AIExitEat);

            if (cus.s_currentState == cus.s_crowdState) ActiveAllObserver(ActionObserver.AICheer);
            else ActiveAllObserver(ActionObserver.AIExitCheer);

            if (cus.anim.GetCurrentAnimatorStateInfo(0).IsName("RightPunch") ||
               cus.anim.GetCurrentAnimatorStateInfo(0).IsName("LeftPunch"))
            {
                if (cus.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.2f)
                {
                    ActiveAllObserver(ActionObserver.AIPunch);
                }
            }

        }
        else if (state is EmployeeStateManager)
        {
            EmployeeStateManager emp = (EmployeeStateManager)state;
            if (emp.agent.velocity != Vector3.zero) ActiveAllObserver(ActionObserver.AIWalk);
            else ActiveAllObserver(ActionObserver.AIExitWalk);
        }
        else if (state is SheriffStateManager)
        {
            SheriffStateManager she = (SheriffStateManager)state;
            if (she.agent.velocity != Vector3.zero) ActiveAllObserver(ActionObserver.AIWalk);
            else ActiveAllObserver(ActionObserver.AIExitWalk);
        }
    }
}
