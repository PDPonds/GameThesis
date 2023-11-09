using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AddRemoveEmpType { AddCooking, AddServe, RemoveCooking, RemoveServe };

public class AddAndRemoveEmp : MonoBehaviour, IInteracable
{
    public AddRemoveEmpType type;

    public void Interaction()
    {
        switch (type)
        {
            case AddRemoveEmpType.AddCooking:
                RestaurantManager.Instance.AddCurrentCookingCount();
                break;
            case AddRemoveEmpType.AddServe:
                RestaurantManager.Instance.AddCurrentServeCount();
                break;
            case AddRemoveEmpType.RemoveCooking:
                RestaurantManager.Instance.RemoveCurrentCookingCount();
                break;
            case AddRemoveEmpType.RemoveServe:
                RestaurantManager.Instance.RemoveCurrentServeCount();
                break;
            default: break;
        }
    }

    public string InteractionText()
    {
        string text = string.Empty;
        switch (type)
        {
            case AddRemoveEmpType.AddCooking:
                text = "Add Cooker";
                break;
            case AddRemoveEmpType.AddServe:
                text = "Add Waiter";
                break;
            case AddRemoveEmpType.RemoveCooking:
                text = "Remove Cooker";
                break;
            case AddRemoveEmpType.RemoveServe:
                text = "Remove Waiter";
                break;
            default: break;
        }
        return text;
    }
}
