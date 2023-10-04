using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuard : MainObserver
{
    [HideInInspector] public float f_currentDelay;
    float f_currentTime;

    void Update()
    {
        if (!PlayerManager.Instance.b_canGuard && !PlayerManager.Instance.b_isGuard)
        {
            f_currentDelay -= Time.deltaTime;
            if (f_currentDelay <= 0)
            {
                PlayerManager.Instance.b_canGuard = true;
            }
        }

        if (PlayerManager.Instance.b_isGuard)
        {
            f_currentTime += Time.deltaTime;
            {
                if (f_currentTime >= PlayerManager.Instance.f_gaurdTime)
                {
                    PlayerManager.Instance.b_isGuard = false;
                    f_currentTime = 0;
                }
            }
        }

        ActiveAllObserver(ActionObserver.PlayerGuard);

    }
}
