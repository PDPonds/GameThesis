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

        if (PlayerManager.Instance.b_canPunch)
        {
            PlayerManager.Instance.i_atkCount++;
            if (FightingManager.Instance.fighter.Count > 0)
            {
                Vector3 playerPos = PlayerManager.Instance.transform.position;
                if (FightingManager.Instance.GetCurrentFightWithPlayer(out CustomerStateManager cus))
                {
                    Rigidbody rb = PlayerManager.Instance.c_rb;
                    Vector3 dir = cus.transform.position - playerPos;
                    dir = dir.normalized;
                    rb.velocity = Vector3.zero;
                    rb.AddForce(dir * PlayerManager.Instance.f_attackMoveForce, ForceMode.Impulse);

                }
                PlayerManager.Instance.b_lockTarget = true;
            }

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

