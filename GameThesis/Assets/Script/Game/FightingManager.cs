using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FightingManager : Auto_Singleton<FightingManager>
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

        foreach (CustomerStateManager cus in fighter)
        {
            if (cus.s_currentState != cus.s_fightState && cus.s_currentState != cus.s_hurtState
                && cus.s_currentState != cus.s_deadState)
            {
                cus.SwitchState(cus.s_fightState);
            }
        }

        if (fighter.Count > 0)
        {
            for (int i = 0; i < fighter.Count; i++)
            {
                CustomerStateManager cus = fighter[i];
                if (!HasFightWithPlayer())
                {
                    cus.b_fightWithPlayer = true;
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

    public bool HasFightWithPlayer()
    {
        if (fighter.Count > 0)
        {
            for (int i = 0; i < fighter.Count; i++)
            {
                if (fighter[i].b_fightWithPlayer)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool GetCurrentFighter(out CustomerStateManager cus)
    {
        if (fighter.Count > 0)
        {
            for (int i = 0; i < fighter.Count; i++)
            {
                if (fighter[i].b_fightWithPlayer)
                {
                    cus = fighter[i];
                    return true;
                }
            }
        }
        cus = null;
        return false;
    }
}
