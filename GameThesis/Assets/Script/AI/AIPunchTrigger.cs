using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPunchTrigger : MainObserver
{
    private void OnTriggerEnter(Collider other)
    {

        ActiveAllObserver(ActionObserver.AIPunch);
        if (other.TryGetComponent(out PlayerManager player))
        {
            if (!player.b_isGuard)
            {
                if (player.TakeDamageAndDead())
                {
                    if (transform.GetComponentInParent<StateManager>())
                    {
                        StateManager state = transform.GetComponentInParent<StateManager>();
                        if (state is CustomerStateManager)
                        {
                            CustomerStateManager customerStateManager = transform.GetComponentInParent<CustomerStateManager>();
                            customerStateManager.SwitchState(customerStateManager.s_activityState);
                        }
                    }
                }
                ActiveAllObserver(ActionObserver.AIPunchHit);
            }
            else
            {
                ActiveAllObserver(ActionObserver.AIPunchHitBlock);
            }
        }

        if (transform.GetComponentInParent<StateManager>())
        {
            StateManager state = transform.GetComponentInParent<StateManager>();
            if (state is CustomerStateManager)
            {
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
}

