using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCombat : MainObserver
{

    private void Update()
    {
        if (PlayerManager.Instance.b_isHold)
        {
            PlayerManager.Instance.f_currentHoldTime += Time.deltaTime;

            if (PlayerManager.Instance.f_currentHoldTime >= PlayerManager.Instance.f_heavyPunchTime)
            {
                ActiveAllObserver(ActionObserver.PlayerHeavyPunch);
                PlayerManager.Instance.f_currentHoldTime = 0;
                PlayerManager.Instance.b_isHold = false;
                PlayerManager.Instance.b_canPunch = false;
                PlayerManager.Instance.f_currentPunchDelay = PlayerManager.Instance.f_punchDelay;
            }
        }
        else
        {
            if (PlayerManager.Instance.f_currentHoldTime >= PlayerManager.Instance.f_softPunchTime)
            {
                PlayerManager.Instance.i_atkCount++;

                if (PlayerManager.Instance.i_atkCount % 2 == 0)
                {
                    ActiveAllObserver(ActionObserver.PlayerRightSoftPunch);
                }
                else
                {
                    ActiveAllObserver(ActionObserver.PlayerLeftSoftPunch);
                }

                PlayerManager.Instance.f_currentHoldTime = 0;
                PlayerManager.Instance.b_isHold = false;
                PlayerManager.Instance.b_canPunch = false;
                PlayerManager.Instance.f_currentPunchDelay = PlayerManager.Instance.f_punchDelay;

            }
        }

        if (!PlayerManager.Instance.b_canPunch)
        {
            PlayerManager.Instance.f_currentPunchDelay -= Time.deltaTime;
            if (PlayerManager.Instance.f_currentPunchDelay <= 0)
            {
                PlayerManager.Instance.b_canPunch = true;
            }
        }

    }

}
