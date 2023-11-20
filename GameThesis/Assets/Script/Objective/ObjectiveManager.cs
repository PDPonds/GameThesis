using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : Auto_Singleton<ObjectiveManager>
{
    public List<ObjectiveSO> allObjectives = new List<ObjectiveSO>();

    public int i_currentObjectiveID;

    ObjectiveSO s_currentObjective;

    public MainObjective mainObjtive;

    private void Update()
    {
        if (GetCurrentObjective(out int index) && index < allObjectives.Count)
        {
            s_currentObjective = allObjectives[index];
        }
        else s_currentObjective = null;

        if (s_currentObjective != null)
        {
            switch (s_currentObjective.e_type)
            {
                case ObjectiveType.CollectMoney:

                    CollectMoneyObjective collectMoney = (CollectMoneyObjective)s_currentObjective;

                    if (GameManager.Instance.f_pocketMoney >= collectMoney.f_tagetMoney)
                    {
                        NextObjective();
                    }

                    break;
                case ObjectiveType.UnlockNewItem:

                    MenuHandler menuHandler = RestaurantManager.Instance.menuHandler;

                    UnlockNewItemObjective unlock = (UnlockNewItemObjective)s_currentObjective;
                    switch (unlock.e_item)
                    {
                        case UnlockNewItemObjective.ItemType.Table:

                            if (RestaurantManager.Instance.GetCurrentTableIsReadyCount() >= unlock.i_targetCount)
                            {
                                NextObjective();
                            }

                            break;
                        case UnlockNewItemObjective.ItemType.Dish:

                            if (menuHandler.DishStatus(unlock.i_menuIndex))
                            {
                                NextObjective();
                            }

                            break;
                        case UnlockNewItemObjective.ItemType.Drink:

                            if (menuHandler.DrinkStatus(unlock.i_menuIndex))
                            {
                                NextObjective();
                            }

                            break;
                        default: break;
                    }

                    break;
                case ObjectiveType.UpgradeRestaurant:

                    //UpgradeRestaurantObjective upgrade = (UpgradeRestaurantObjective)s_currentObjective;
                    //if (RestaurantManager.Instance.i_level >= upgrade.i_targetLevel)
                    //{
                    //    NextObjective();
                    //}

                    break;
                default: break;
            }
        }

    }

    public void NextObjective()
    {
        i_currentObjectiveID++;
    }

    public bool GetCurrentObjective(out int index)
    {
        for (int i = 0; i < allObjectives.Count; i++)
        {
            if (allObjectives[i].i_objectiveID == i_currentObjectiveID)
            {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }

}
