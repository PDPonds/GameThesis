using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCombat : MainObserver
{
    [SerializeField] float f_delayTime;
    [SerializeField] float f_holdTime;

    private void Update()
    {

        PlayerManager.Instance.f_holdMoveSpeed = PlayerManager.Instance.f_moveSpeed * .7f;

        if (PlayerManager.Instance.b_isHold)
        {
            if (f_holdTime < PlayerManager.Instance.f_maxHoldTime)
            {
                f_holdTime += Time.deltaTime;
            }
        }

        if (!PlayerManager.Instance.b_canPunch)
        {
            f_delayTime -= Time.deltaTime;
            if (f_delayTime < 0)
            {
                PlayerManager.Instance.b_canPunch = true;
            }
        }

        ActiveAllObserver(ActionObserver.PlayerHoldPunch);
    }

    public void holdButton()
    {
        if (PlayerManager.Instance.b_canPunch)
        {
            PlayerManager.Instance.b_isHold = true;
            PlayerManager.Instance.f_moveSpeed = PlayerManager.Instance.f_holdMoveSpeed;
        }
    }

    public void releaseHoldButton()
    {
        if (PlayerManager.Instance.b_isHold)
        {
            if (f_holdTime >= PlayerManager.Instance.f_holdTimeToPunch)
            {
                SetUpDalayTime(PlayerManager.Instance.f_fistDelay);
                ActiveAllObserver(ActionObserver.PlayerPunch);
                PlayerManager.Instance.c_punchCol.enabled = true;
            }
            else
            {
                SetUpDalayTime(PlayerManager.Instance.f_fistDelay / 2f);
            }

            PlayerManager.Instance.f_moveSpeed = PlayerManager.Instance.f_walkSpeed;
            PlayerManager.Instance.b_canPunch = false;
            PlayerManager.Instance.b_isHold = false;
            f_holdTime = 0;
        }

    }

    void SetUpDalayTime(float time)
    {
        f_delayTime = time;
    }

}
