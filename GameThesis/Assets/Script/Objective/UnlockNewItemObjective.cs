using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective/UnlockNewItem")]
public class UnlockNewItemObjective : ObjectiveSO
{
    public int i_targetCount;
    public ItemType e_item;
    public int i_menuIndex;

    public UnlockNewItemObjective()
    {
        e_type = ObjectiveType.UnlockNewItem;
    }

    public enum ItemType { Table, Dish, Drink }
}
