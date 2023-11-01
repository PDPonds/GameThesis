using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvent : MonoBehaviour
{
    public void EC()
    {
        CustomerStateManager cus = GetComponent<CustomerStateManager>();
        cus.c_atkCol.enabled = true;
    }

    public void DC()
    {
        CustomerStateManager cus = GetComponent<CustomerStateManager>();
        cus.c_atkCol.enabled = false;
    }
}
