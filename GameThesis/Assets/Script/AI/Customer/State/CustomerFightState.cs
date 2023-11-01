using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerFightState : BaseState
{
    Vector3 playerPos;

    bool b_isRightDir;
    float f_reDir;
    float f_currentReDirection;

    float f_currentDelay;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

        f_currentDelay = cus.f_atkDelay;

        f_reDir = Random.Range(5f, 7f);
        f_currentReDirection = f_reDir;

        
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        cus.b_inFight = true;

        cus.RagdollOff();

        cus.agent.speed = cus.f_walkSpeed;

        playerPos = PlayerManager.Instance.transform.position;
        Vector3 fightPos = playerPos - (cus.transform.forward * cus.f_fightDis);

        Vector3 rightSpeed = cus.transform.right * cus.f_walkSpeed;
        Vector3 leftSpeed = -cus.transform.right * cus.f_walkSpeed;
        Vector3 waitPos;

        if (b_isRightDir)
        {
            waitPos = playerPos - (cus.transform.forward * cus.f_waitDis) + rightSpeed;
            f_currentReDirection -= Time.deltaTime;
            if (f_currentReDirection <= 0)
            {
                b_isRightDir = !b_isRightDir;
                f_reDir = Random.Range(5f, 7f);
                f_currentReDirection = f_reDir;
            }
        }
        else
        {
            waitPos = playerPos - (cus.transform.forward * cus.f_waitDis) + leftSpeed;
            f_currentReDirection -= Time.deltaTime;
            if (f_currentReDirection <= 0)
            {
                b_isRightDir = !b_isRightDir;
                f_reDir = Random.Range(5f, 7f);
                f_currentReDirection = f_reDir;
            }
        }

        if (cus.b_fightWithPlayer)
        {
            cus.ApplyOutlineColor(cus.color_fightWithPlayer, cus.f_outlineScale);
            //Set Position
            if (cus.transform.position != fightPos)
            {
                cus.agent.SetDestination(fightPos);
            }

            //Attack Range

            if (Vector3.Distance(cus.transform.position, playerPos) < cus.f_attackRange)
            {
                f_currentDelay -= Time.deltaTime;
            }

            //Attack
            if (f_currentDelay <= 0)
            {
                Attack(cus);
                f_currentDelay = cus.f_atkDelay;
            }
        }
        else
        {
            cus.ApplyOutlineColor(cus.color_inFighting, cus.f_outlineScale);
            if (cus.transform.position != waitPos)
            {
                cus.agent.SetDestination(waitPos);
            }
        }

        Vector3 lookDir = playerPos - cus.transform.position;
        lookDir = lookDir.normalized;
        if (lookDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            Quaternion rot = Quaternion.Slerp(cus.transform.rotation, targetRot, 10 * Time.deltaTime);
            rot.x = 0f;
            rot.z = 0f;
            cus.transform.rotation = rot;
        }

        cus.anim.SetBool("fightState", true);
        cus.anim.SetBool("walk", false);
        cus.anim.SetBool("run", false);
        cus.anim.SetBool("sit", false);
    }

    void Attack(CustomerStateManager cus)
    {
        cus.i_atkCount++;
        if (cus.i_atkCount % 2 == 0)
        {
            cus.anim.Play("LeftPunch");
        }
        else
        {
            cus.anim.Play("RightPunch");
        }

        cus.agent.velocity = Vector3.zero;

    }


}
