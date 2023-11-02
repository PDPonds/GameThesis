using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPunchTrigger : MainObserver
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerManager>(out PlayerManager player))
            {
                if (!player.b_isGuard)
                {
                    ActiveAllObserver(ActionObserver.AIPunchHit);
                    if (player.TakeDamageAndDead())
                    {
                        for (int i = 0; i < RestaurantManager.Instance.allSheriffs.Length; i++)
                        {
                            SheriffStateManager shrSM = RestaurantManager.Instance.allSheriffs[i];
                            if (shrSM.s_currentState == shrSM.s_waitForFightEnd)
                            {
                                shrSM.SwitchState(shrSM.s_activityState);
                            }
                        }

                        foreach (CustomerStateManager cus in RestaurantManager.Instance.allCustomers)
                        {
                            if (cus.b_inFight || cus.s_currentState == cus.s_fightState)
                            {
                                cus.b_inFight = false;
                                cus.b_fightWithPlayer = false;
                                cus.SwitchState(cus.s_walkAroundState);
                            }
                        }
                    }
                }
                else
                {
                    ActiveAllObserver(ActionObserver.AIPunchHitBlock);
                }
            }
        }
    }
}
