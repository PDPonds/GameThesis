using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CustomerWalkAroundState : BaseState
{
    float f_currentTimeToWalk;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        cus.i_currentHP = cus.i_maxHP;
        f_currentTimeToWalk = 0;

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        cus.RagdollOff();

        cus.anim.SetBool("fightState", false);
        cus.anim.SetBool("sit", false);
        cus.anim.SetBool("drunk", false);
        cus.anim.SetBool("cheer", false);
        cus.anim.SetBool("eat", false);
        cus.anim.SetBool("checkbill", false);

        f_currentTimeToWalk -= Time.deltaTime;
        if (f_currentTimeToWalk <= 0)
        {
            float xPos = Random.Range(10f, 20f);
            float zPos = Random.Range(-35f, 25f);
            cus.v_walkPos = new Vector3(xPos, 0, zPos);
            f_currentTimeToWalk = cus.f_findNextPositionTime;
        }

        if (cus.b_escape)
        {
            cus.agent.speed = cus.f_runSpeed;
            cus.agent.SetDestination(GameManager.Instance.s_gameState.t_spawnPoint[cus.i_spawnPosIndex].position);
            cus.ApplyOutlineColor(cus.color_warning, cus.f_outlineScale);
            if (cus.agent.velocity != Vector3.zero)
            {
                cus.anim.SetBool("run", true);
            }
            else
            {
                cus.anim.SetBool("run", false);
            }

            if (Vector3.Distance(cus.transform.position, GameManager.Instance.s_gameState.t_spawnPoint[cus.i_spawnPosIndex].position)
                <= 1f)
            {
                cus.b_escape = false;
            }

        }
        else
        {
            cus.agent.speed = cus.f_walkSpeed;
            cus.agent.SetDestination(cus.v_walkPos);
            Color noColor = new Color(0, 0, 0, 0);
            cus.ApplyOutlineColor(noColor, 0f);

            if (cus.agent.velocity != Vector3.zero)
            {
                cus.anim.SetBool("walk", true);
                cus.anim.SetBool("run", false);
            }
            else
            {
                cus.anim.SetBool("walk", false);
            }
        }
    }


}
