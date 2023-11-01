using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CustomerWalkAroundState : BaseState
{
    float f_currentTimeToWalk;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.i_currentHP = customerStateManager.i_maxHP;
        f_currentTimeToWalk = 0;

        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);
        customerStateManager.anim.SetBool("cheer", false);

        f_currentTimeToWalk -= Time.deltaTime;
        if (f_currentTimeToWalk <= 0)
        {
            float xPos = Random.Range(10f, 20f);
            float zPos = Random.Range(-35f, 25f);
            customerStateManager.v_walkPos = new Vector3(xPos, 0, zPos);
            f_currentTimeToWalk = customerStateManager.f_findNextPositionTime;
        }

        if (customerStateManager.b_escape)
        {
            customerStateManager.agent.speed = customerStateManager.f_runSpeed;
            customerStateManager.agent.SetDestination(GameManager.Instance.s_gameState.t_spawnPoint[customerStateManager.i_spawnPosIndex].position);
            customerStateManager.ApplyOutlineColor(customerStateManager.color_warning, customerStateManager.f_outlineScale);
            if (customerStateManager.agent.velocity != Vector3.zero)
            {
                customerStateManager.anim.SetBool("run", true);
            }
            else
            {
                customerStateManager.anim.SetBool("run", false);
            }

            if (Vector3.Distance(customerStateManager.transform.position, GameManager.Instance.s_gameState.t_spawnPoint[customerStateManager.i_spawnPosIndex].position)
                <= 1f)
            {
                customerStateManager.b_escape = false;
            }

        }
        else
        {
            customerStateManager.agent.speed = customerStateManager.f_walkSpeed;
            customerStateManager.agent.SetDestination(customerStateManager.v_walkPos);
            Color noColor = new Color(0, 0, 0, 0);
            customerStateManager.ApplyOutlineColor(noColor, 0f);

            if (customerStateManager.agent.velocity != Vector3.zero)
            {
                customerStateManager.anim.SetBool("walk", true);
                customerStateManager.anim.SetBool("run", false);
            }
            else
            {
                customerStateManager.anim.SetBool("walk", false);
            }
        }
    }


}
