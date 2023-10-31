using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingManager : MonoBehaviour
{
    public List<CustomerStateManager> fighter = new List<CustomerStateManager>();

    private void Update()
    {
        if (fighter.Count != FighterCount())
        {
            fighter.Clear();
            for (int i = 0; i < RestaurantManager.Instance.allCustomers.Length; i++)
            {
                CustomerStateManager cus = RestaurantManager.Instance.allCustomers[i];
                if (cus.b_inFight)
                {
                    fighter.Add(cus);
                }
            }
        }


    }

    public int FighterCount()
    {
        int fighterCount = 0;
        if (RestaurantManager.Instance.allCustomers.Length > 0)
        {
            for (int i = 0; i < RestaurantManager.Instance.allCustomers.Length; i++)
            {
                CustomerStateManager cus = RestaurantManager.Instance.allCustomers[i];
                if (cus.b_inFight)
                {
                    fighterCount++;
                }
            }
        }

        return fighterCount;
    }

}
