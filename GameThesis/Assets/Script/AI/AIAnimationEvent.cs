using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvent : MonoBehaviour
{
    public void EnableColliderAtk()
    {
        if(transform.TryGetComponent<CustomerStateManager>(out CustomerStateManager customerStateManager))
        {
            customerStateManager.c_atkCollider.enabled = true;
        }
        
        if(transform.TryGetComponent<EmployeeStateManager>(out EmployeeStateManager employeeStateManager))
        {
            employeeStateManager.c_atkCollider.enabled = true;
        }
    }

    public void DisableColliderHand()
    {
        if(transform.TryGetComponent<CustomerStateManager>(out CustomerStateManager customerStateManager))
        {
            customerStateManager.c_atkCollider.enabled = false;
        }
        if(transform.TryGetComponent<EmployeeStateManager>(out EmployeeStateManager employeeStateManager))
        {
            employeeStateManager.c_atkCollider.enabled = false;
        }
        

    }
}
