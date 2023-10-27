using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCombat : MainObserver
{
    private void Update()
    {
        if (!PlayerManager.Instance.b_canPunch)
        {
            PlayerManager.Instance.f_currentPunchDelay -= Time.deltaTime;
            if (PlayerManager.Instance.f_currentPunchDelay <= 0)
            {
                PlayerManager.Instance.b_canPunch = true;
            }
        }
    }

    public void Punch()
    {
        if(PlayerManager.Instance.b_canPunch)
        {
            PlayerManager.Instance.b_inFighting = true;
            PlayerManager.Instance.f_currentInFightingTime = PlayerManager.Instance.f_maxInFightingTime;
            
            PlayerManager.Instance.i_atkCount++;

            if (PlayerManager.Instance.i_atkCount % 2 == 0)
            {
                ActiveAllObserver(ActionObserver.PlayerRightSoftPunch);
            }
            else
            {
                ActiveAllObserver(ActionObserver.PlayerLeftSoftPunch);
            }

            PlayerManager.Instance.b_canPunch = false;
            PlayerManager.Instance.f_currentPunchDelay = PlayerManager.Instance.f_punchDelay;
        }
        
    }

}
