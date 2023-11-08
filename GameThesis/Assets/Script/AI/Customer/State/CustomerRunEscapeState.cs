using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerRunEscapeState : BaseState
{
    float f_currentRunTime;
    Vector3 v_walkPos;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        f_currentRunTime = cus.f_runTime;

        float xPos = Random.Range(10f, 20f);
        float zPos = Random.Range(-13f, 13f);
        v_walkPos = new Vector3(xPos, 0, zPos);

        Color noColor = new Color(0, 0, 0, 0);
        cus.ApplyOutlineColor(noColor, 0f);

        cus.b_escape = false;
        cus.b_isDrunk = false;

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        f_currentRunTime -= Time.deltaTime;
        if (f_currentRunTime <= 0)
        {
            cus.SwitchState(cus.s_walkAroundState);
        }

        if (Vector3.Distance(cus.transform.position, GameManager.Instance.s_gameState.t_spawnPoint[cus.i_spawnPosIndex].position) <= 1f)
        {
            cus.agent.velocity = Vector3.zero;
            cus.anim.SetBool("run", false);
        }
        else
        {
            cus.agent.SetDestination(GameManager.Instance.s_gameState.t_spawnPoint[cus.i_spawnPosIndex].position);
            cus.anim.SetBool("run", true);

        }
        cus.agent.speed = cus.f_runSpeed;

    }
}
