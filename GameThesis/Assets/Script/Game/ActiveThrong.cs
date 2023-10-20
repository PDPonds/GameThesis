using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveThrong : MonoBehaviour
{
    private void Update()
    {
        Collider[] cus = Physics.OverlapSphere(transform.position, GameManager.Instance.f_throngArea, GameManager.Instance.lm_enemyMask);
        if (RestaurantManager.Instance.HasCustomerInFightState())
        {
            transform.position = PlayerManager.Instance.transform.position;
            if (cus.Length > 0)
            {
                for (int i = 0; i < cus.Length; i++)
                {
                    if (cus[i].transform.parent != null)
                    {
                        CustomerStateManager cusMan = cus[i].GetComponentInParent<CustomerStateManager>();
                        if (cusMan != null)
                        {
                            if (cusMan.currentAreaStay == AreaType.OutRestaurant &&
                                cusMan.s_currentState == cusMan.s_walkAroundState)
                            {
                                cusMan.SwitchState(cusMan.s_throngState);

                            }
                        }
                        SheriffStateManager shrMan = cus[i].GetComponentInParent<SheriffStateManager>();
                        if (shrMan != null)
                        {
                            if (shrMan.s_currentState == shrMan.s_activityState)
                            {
                                shrMan.SwitchState(shrMan.s_waitForFightEnd);
                            }
                        }
                    }
                    else
                    {
                        if (cus[i].TryGetComponent<CustomerStateManager>(out CustomerStateManager cusMan))
                        {
                            if (cusMan.currentAreaStay == AreaType.OutRestaurant &&
                                cusMan.s_currentState == cusMan.s_walkAroundState)
                            {
                                cusMan.SwitchState(cusMan.s_throngState);
                            }
                        }
                        else if (cus[i].TryGetComponent<SheriffStateManager>(out SheriffStateManager shrMan))
                        {
                            if (shrMan.s_currentState == shrMan.s_activityState)
                            {
                                shrMan.SwitchState(shrMan.s_waitForFightEnd);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < RestaurantManager.Instance.allCustomers.Length; i++)
            {
                CustomerStateManager cusSM = RestaurantManager.Instance.allCustomers[i];
                if (cusSM.s_currentState == cusSM.s_throngState)
                {
                    cusSM.SwitchState(cusSM.s_walkAroundState);
                }
            }
            for (int i = 0; i < RestaurantManager.Instance.allSheriffs.Length; i++)
            {
                SheriffStateManager shrSM = RestaurantManager.Instance.allSheriffs[i];
                if (shrSM.s_currentState == shrSM.s_waitForFightEnd)
                {
                    shrSM.SwitchState(shrSM.s_activeThrong);
                }
            }
            GameManager.Instance.DisableThrong();
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.f_throngDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.f_throngArea);
    }
}
