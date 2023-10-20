using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffActivityState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        SheriffStateManager sheriffStateManager = (SheriffStateManager)ai;
        sheriffStateManager.g_sheriffMesh.SetActive(true);
        sheriffStateManager.i_walkCount++;
    }

    public override void UpdateState(StateManager ai)
    {
        SheriffStateManager sheriffStateManager = (SheriffStateManager)ai;
        if (sheriffStateManager.i_walkCount % 2 == 0)
        {
            sheriffStateManager.agent.SetDestination(GameManager.Instance.s_gameState.t_spawnPoint[1].position);
            sheriffStateManager.anim.SetBool("walk", true);
            sheriffStateManager.anim.SetBool("run", false);
            
            if (Vector3.Distance(sheriffStateManager.transform.position, GameManager.Instance.s_gameState.t_spawnPoint[1].position)
                <= 1f)
            {
                sheriffStateManager.SwitchState(sheriffStateManager.s_visibleState);
            }
        }
        else
        {
            sheriffStateManager.agent.SetDestination(GameManager.Instance.s_gameState.t_spawnPoint[0].position);
            sheriffStateManager.anim.SetBool("walk", true);
            sheriffStateManager.anim.SetBool("run", false);

            if (Vector3.Distance(sheriffStateManager.transform.position, GameManager.Instance.s_gameState.t_spawnPoint[0].position)
                <= 1f)
            {
                sheriffStateManager.SwitchState(sheriffStateManager.s_visibleState);
            }
        }
    }
}
