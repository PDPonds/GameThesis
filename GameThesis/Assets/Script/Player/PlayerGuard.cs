using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuard : MainObserver
{
    void Update()
    {
        if (PlayerManager.Instance.b_isGuard)
        {
            PlayerManager.Instance.f_currentGuardDelay = PlayerManager.Instance.f_guardDelay;
            PlayerManager.Instance.f_currentGuardTime += Time.deltaTime;

            if (PlayerManager.Instance.f_currentGuardTime >= PlayerManager.Instance.f_guardTime)
            {
                PlayerManager.Instance.b_isGuard = false;
                PlayerManager.Instance.b_canGuard = false;
                PlayerManager.Instance.f_currentGuardTime = 0;
            }
        }
        else
        {
            PlayerManager.Instance.f_currentGuardTime = 0;
            if (!PlayerManager.Instance.b_canGuard)
            {
                PlayerManager.Instance.f_currentGuardDelay -= Time.deltaTime;
                if (PlayerManager.Instance.f_currentGuardDelay <= 0)
                {
                    PlayerManager.Instance.b_canGuard = true;
                }
            }
        }
        ActiveAllObserver(ActionObserver.PlayerGuard);

    }
}
