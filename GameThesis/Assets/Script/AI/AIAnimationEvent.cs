using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvent : MonoBehaviour
{
    public void EnableColliderLeftHand()
    {
        if(transform.TryGetComponent<CustomerStateManager>(out CustomerStateManager customerStateManager))
        {
            customerStateManager.c_leftHandPunch.enabled = true;
        }
        
        if(transform.TryGetComponent<EmployeeStateManager>(out EmployeeStateManager employeeStateManager))
        {
            employeeStateManager.c_leftHandPunch.enabled = true;
        }
    }

    public void DisableColliderHand()
    {
        if(transform.TryGetComponent<CustomerStateManager>(out CustomerStateManager customerStateManager))
        {
            customerStateManager.c_leftHandPunch.enabled = false;
            customerStateManager.c_rightHandPunch.enabled = false;
        }
        if(transform.TryGetComponent<EmployeeStateManager>(out EmployeeStateManager employeeStateManager))
        {
            employeeStateManager.c_leftHandPunch.enabled = false;
            employeeStateManager.c_rightHandPunch.enabled = false;
        }
        

    }

    public void EnableColliderRightHand()
    {
        if(transform.TryGetComponent<CustomerStateManager>(out CustomerStateManager customerStateManager))
        {
            customerStateManager.c_rightHandPunch.enabled = true;
        }
        if(transform.TryGetComponent<EmployeeStateManager>(out EmployeeStateManager employeeStateManager))
        {
            employeeStateManager.c_rightHandPunch.enabled = true;
        }

    }
}
