using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaType { OutRestaurant, InRestaurant }

public class AreaTrigger : MonoBehaviour
{
    public AreaType areaType;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent != null)
        {
            if (other.CompareTag("Player"))
            {
                PlayerManager player = other.GetComponentInParent<PlayerManager>();
                if (player != null)
                {
                    if (player.currentAreaStay != areaType)
                    {
                        player.currentAreaStay = areaType;
                    }
                }
            }
            else if (other.CompareTag("Enemy"))
            {
                StateManager state = other.GetComponentInParent<StateManager>();
                if (state != null)
                {
                    if (state is CustomerStateManager)
                    {
                        CustomerStateManager cus = (CustomerStateManager)state;
                        if (cus.currentAreaStay != areaType)
                        {
                            cus.currentAreaStay = areaType;
                        }
                    }
                    else if (state is EmployeeStateManager)
                    {
                        EmployeeStateManager emp = (EmployeeStateManager)state;
                        if (emp.currentAreaStay != areaType)
                        {
                            emp.currentAreaStay = areaType;
                        }
                    }
                }
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                if (other.TryGetComponent<PlayerManager>(out PlayerManager player))
                {
                    if (player.currentAreaStay != areaType)
                    {
                        player.currentAreaStay = areaType;
                    }
                }
            }
            else if (other.CompareTag("Enemy"))
            {
                if (other.TryGetComponent<StateManager>(out StateManager state))
                {
                    if (state is CustomerStateManager)
                    {
                        CustomerStateManager cus = (CustomerStateManager)state;
                        if (cus.currentAreaStay != areaType)
                        {
                            cus.currentAreaStay = areaType;
                        }
                    }
                    else if (state is EmployeeStateManager)
                    {
                        EmployeeStateManager emp = (EmployeeStateManager)state;
                        if (emp.currentAreaStay != areaType)
                        {
                            emp.currentAreaStay = areaType;
                        }
                    }
                }
            }
        }
    }

}
