using UnityEngine;
using UnityEngine.AI;

public class CustomerWalkAroundState : BaseState
{
    float f_currentTimeToWalk;

    public override void EnterState(StateManager ai)
    {
        Debug.Log("Customer : WalkAround");
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.i_currentHP = customerStateManager.i_maxHP;
        f_currentTimeToWalk = 0;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.RagdollOff();

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.DisablePunch();

        if (!customerStateManager.b_canAtk)
        {
            customerStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (customerStateManager.f_currentAtkDelay <= 0)
            {
                customerStateManager.b_canAtk = true;
            }
        }

        f_currentTimeToWalk -= Time.deltaTime;
        if (f_currentTimeToWalk <= 0)
        {
            float xPos = Random.Range(10f, 20f);
            float zPos = Random.Range(-13f, 13f);
            customerStateManager.v_walkPos = new Vector3(xPos,0,zPos);
            f_currentTimeToWalk = customerStateManager.f_findNextPositionTime;
        }

        customerStateManager.agent.SetDestination(customerStateManager.v_walkPos);

        if(customerStateManager.agent.velocity != Vector3.zero)
        {
            customerStateManager.anim.SetBool("walk", true);
        }
        else
        {
            customerStateManager.anim.SetBool("walk", false);
        }

    }


}
