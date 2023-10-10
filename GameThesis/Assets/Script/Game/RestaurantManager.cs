using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    public CustomerStateManager[] allCustomer;
    public EmployeeStateManager[] allEmployee;

    void Update()
    {
       allCustomer = FindObjectsOfType<CustomerStateManager>();
       allEmployee = FindObjectsOfType<EmployeeStateManager>();
       
    }
}
