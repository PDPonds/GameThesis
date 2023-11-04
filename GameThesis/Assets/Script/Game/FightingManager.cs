using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FightingManager : Auto_Singleton<FightingManager>
{
    public List<CustomerStateManager> fighter = new List<CustomerStateManager>();

    public WaypointIndicator s_wayPoint;

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
                    GetFighterClose(out CustomerStateManager nextCus);
                    if (nextCus != null)
                    {
                        nextCus.b_fightWithPlayer = true;
                    }
                }
            }
        }

        if (GetCurrentFightWithPlayer(out CustomerStateManager cusfight))
        {
            s_wayPoint.gameObject.SetActive(true);
            s_wayPoint.target = cusfight.t_mesh;
            s_wayPoint.image.color = cusfight.color_fightWithPlayer;
        }
        else
        {
            s_wayPoint.gameObject.SetActive(false);
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

    public bool GetCurrentFightWithPlayer(out CustomerStateManager cus)
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

    void GetFighterClose(out CustomerStateManager cus)
    {
        CustomerStateManager close = null;
        float closeFloat = 0;
        if (fighter.Count > 0)
        {
            for (int i = 0; i < fighter.Count; i++)
            {
                Vector3 figterPos = fighter[i].transform.position;
                Vector3 playerPos = PlayerManager.Instance.transform.position;
                if (close == null)
                {
                    close = fighter[i];
                    closeFloat = Vector3.Distance(figterPos, playerPos);
                }
                else
                {
                    float currentDis = Vector3.Distance(figterPos, playerPos);
                    if (currentDis < closeFloat)
                    {
                        close = fighter[i];
                        closeFloat = currentDis;
                    }
                }
            }
        }

        cus = close;
    }
}
