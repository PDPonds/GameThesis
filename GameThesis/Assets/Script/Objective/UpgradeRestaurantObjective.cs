using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective/UpgradeRestaurant")]
public class UpgradeRestaurantObjective : ObjectiveSO
{
    public int i_targetLevel;

    public UpgradeRestaurantObjective()
    {
        e_type = ObjectiveType.UpgradeRestaurant;
    }
}
