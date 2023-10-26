using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveCrowd : MonoBehaviour
{
    private void OnEnable()
    {
        transform.position = PlayerManager.Instance.transform.position;
    }

    private void Update()
    {
        Collider[] cus = Physics.OverlapSphere(transform.position, GameManager.Instance.f_crowdArea, GameManager.Instance.lm_enemyMask);
        if (RestaurantManager.Instance.HasCustomerInFightState())
        {

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
                                float theta = (2 * Mathf.PI / (cus.Length)) * i;
                                float xPos = Mathf.Sin(theta) * GameManager.Instance.f_crowdDistance;
                                float yPos = Mathf.Cos(theta) * GameManager.Instance.f_crowdDistance;
                                Vector3 xyPos = new Vector3(xPos, 0, yPos) + transform.position;
                                Vector3 throngPos = xyPos;

                                cusMan.v_crowdPos = throngPos;

                                cusMan.SwitchState(cusMan.s_crowdState);
                            }
                        }
                        SheriffStateManager shrMan = cus[i].GetComponentInParent<SheriffStateManager>();
                        if (shrMan != null)
                        {
                            if (shrMan.s_currentState == shrMan.s_activityState &&
                                PlayerManager.Instance.currentAreaStay == AreaType.OutRestaurant)
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
                                float theta = (2 * Mathf.PI / (cus.Length)) * i;
                                float xPos = Mathf.Sin(theta) * GameManager.Instance.f_crowdDistance;
                                float yPos = Mathf.Cos(theta) * GameManager.Instance.f_crowdDistance;
                                Vector3 xyPos = new Vector3(xPos, 0, yPos) + transform.position;
                                Vector3 throngPos = xyPos;

                                cusMan.v_crowdPos = throngPos;

                                cusMan.SwitchState(cusMan.s_crowdState);
                            }
                        }
                        else if (cus[i].TryGetComponent<SheriffStateManager>(out SheriffStateManager shrMan))
                        {
                            if (shrMan.s_currentState == shrMan.s_activityState &&
                                PlayerManager.Instance.currentAreaStay == AreaType.OutRestaurant)
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
                if (cusSM.s_currentState == cusSM.s_crowdState)
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
            GameManager.Instance.DisableCrowd();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.f_crowdDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.f_crowdArea);
    }
}
