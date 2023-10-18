using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPunchTrigger : MainObserver
{
    private void OnTriggerStay(Collider other)
    {
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
                            customerStateManager.SwitchState(customerStateManager.s_walkAroundState);
                        }
                        if (state is EmployeeStateManager)
                        {
                            EmployeeStateManager employeeStateManager = transform.GetComponentInParent<EmployeeStateManager>();
                            employeeStateManager.SwitchState(employeeStateManager.s_activityState);
                        }
                    }
                }
                else
                {
                    Debug.Log("TakeDamage");
                    if (transform.GetComponentInParent<StateManager>())
                    {
                        StateManager state = transform.GetComponentInParent<StateManager>();
                        if (state is CustomerStateManager)
                        {
                            CustomerStateManager customerStateManager = transform.GetComponentInParent<CustomerStateManager>();
                            customerStateManager.SwitchState(customerStateManager.s_fightState);
                        }
                        if (state is EmployeeStateManager)
                        {
                            EmployeeStateManager employeeStateManager = transform.GetComponentInParent<EmployeeStateManager>();
                            employeeStateManager.SwitchState(employeeStateManager.s_fightState);
                        }
                    }
                }
                ActiveAllObserver(ActionObserver.AIPunchHit);
            }
            else
            {
                ActiveAllObserver(ActionObserver.AIPunchHitBlock);
                if (transform.GetComponentInParent<StateManager>())
                {
                    StateManager state = transform.GetComponentInParent<StateManager>();
                    if (state is CustomerStateManager)
                    {
                        CustomerStateManager customerStateManager = transform.GetComponentInParent<CustomerStateManager>();
                        customerStateManager.SwitchState(customerStateManager.s_fightState);
                    }
                    if (state is EmployeeStateManager)
                    {
                        EmployeeStateManager employeeStateManager = transform.GetComponentInParent<EmployeeStateManager>();
                        employeeStateManager.SwitchState(employeeStateManager.s_fightState);
                    }
                }
            }
        }


        if (transform.GetComponentInParent<StateManager>())
        {
            StateManager state = transform.GetComponentInParent<StateManager>();
            if (state is CustomerStateManager)
            {
                CustomerStateManager customerStateManager = transform.GetComponentInParent<CustomerStateManager>();

                customerStateManager.c_atkCollider.enabled = false;
            }
            if (state is EmployeeStateManager)
            {
                EmployeeStateManager employeeStateManager = transform.GetComponentInParent<EmployeeStateManager>();
                employeeStateManager.c_atkCollider.enabled = false;

            }
        }


    }
}

