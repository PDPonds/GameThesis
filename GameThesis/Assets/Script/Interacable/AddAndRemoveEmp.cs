using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AddRemoveEmpType { AddCooking, AddServe, RemoveCooking, RemoveServe };

public class AddAndRemoveEmp : MonoBehaviour, IInteracable
{
    public AddRemoveEmpType type;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interaction()
    {
        animator.SetTrigger("Pressed");

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
                text = "[E] Add Cooker";
                break;
            case AddRemoveEmpType.AddServe:
                text = "[E] Add Waiter";
                break;
            case AddRemoveEmpType.RemoveCooking:
                text = "[E] Remove Cooker";
                break;
            case AddRemoveEmpType.RemoveServe:
                text = "[E] Remove Waiter";
                break;
            default: break;
        }
        return text;
    }

    public Color InteractionTextColor()
    {
        return Color.white;
    }
}
