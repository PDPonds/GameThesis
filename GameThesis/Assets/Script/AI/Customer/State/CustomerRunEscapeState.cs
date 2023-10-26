using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerRunEscapeState : BaseState
{
    float f_currentRunTime;
    Vector3 v_walkPos;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        f_currentRunTime = customerStateManager.f_runTime;

        float xPos = Random.Range(10f, 20f);
        float zPos = Random.Range(-13f, 13f);
        v_walkPos = new Vector3(xPos, 0, zPos);

        Color noColor = new Color(0, 0, 0, 0);
        customerStateManager.ApplyOutlineColor(noColor, 0f);

        customerStateManager.b_escape = false;
        customerStateManager.b_isDrunk = false;
        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;
        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        f_currentRunTime -= Time.deltaTime;
        if (f_currentRunTime <= 0)
        {
            customerStateManager.SwitchState(customerStateManager.s_walkAroundState);
        }

        if (Vector3.Distance(customerStateManager.transform.position, GameManager.Instance.s_gameState.t_spawnPoint[customerStateManager.i_spawnPosIndex].position) <= 1f)
        {
            customerStateManager.agent.velocity = Vector3.zero;
            customerStateManager.anim.SetBool("run", false);
        }
        else
        {
            customerStateManager.agent.SetDestination(GameManager.Instance.s_gameState.t_spawnPoint[customerStateManager.i_spawnPosIndex].position);
            customerStateManager.anim.SetBool("run", true);

        }
        customerStateManager.agent.speed = customerStateManager.f_runSpeed;

    }
}
