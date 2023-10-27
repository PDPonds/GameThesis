using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSO : ScriptableObject
{
    public string s_objectiveName;
    public string s_objectiveDescription;
    public int i_objectiveID;
    public ObjectiveType e_type;



}

public enum ObjectiveType { CollectMoney, UnlockNewItem, UpgradeRestaurant }

