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

        if (PlayerManager.Instance.b_canPunch && 
            PlayerManager.Instance.f_currentStamina > PlayerManager.Instance.f_staminaMultiply)
        {
            PlayerManager.Instance.f_currentStamina -= PlayerManager.Instance.f_attackStamina;
            PlayerManager.Instance.i_atkCount++;
            if (FightingManager.Instance.fighter.Count > 0)
            {
                Vector3 playerPos = PlayerManager.Instance.transform.position;
                if (FightingManager.Instance.GetCurrentFightWithPlayer(out CustomerStateManager cus))
                {
                    float playerAndCusDistance = Vector3.Distance(cus.transform.position, playerPos);
                    Collider col = PlayerManager.Instance.c_punchCol;
                    BoxCollider punchCol = (BoxCollider)col;

                    if (playerAndCusDistance > punchCol.size.z)
                    {
                        Rigidbody rb = PlayerManager.Instance.c_rb;
                        Vector3 dir = cus.transform.position - playerPos;
                        dir = dir.normalized;
                        rb.AddForce(dir * PlayerManager.Instance.f_attackMoveForce, ForceMode.Impulse);
                    }

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

