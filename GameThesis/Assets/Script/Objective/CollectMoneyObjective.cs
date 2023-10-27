using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective/CollectMoney")]
public class CollectMoneyObjective : ObjectiveSO
{
    public float f_tagetMoney;

    public CollectMoneyObjective()
    {
        e_type = ObjectiveType.CollectMoney;
    }
}
