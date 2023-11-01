using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{

    void Update()
    {
        if (!PlayerManager.Instance.b_isSprint)
        {
            PlayerManager.Instance.f_moveSpeed = PlayerManager.Instance.f_walkSpeed;
            if (PlayerManager.Instance.f_currentStamina < PlayerManager.Instance.f_maxStamina)
            {
                PlayerManager.Instance.f_currentStamina += PlayerManager.Instance.f_staminaMultiply * Time.deltaTime;
            }

            PlayerAnimation.Instance.animator.SetBool("isRun", false);

        }
        else
        {
            if (PlayerManager.Instance.f_currentStamina > 0)
            {
                PlayerManager.Instance.f_currentStamina -= PlayerManager.Instance.f_staminaMultiply * Time.deltaTime;
                PlayerManager.Instance.f_moveSpeed = PlayerManager.Instance.f_runSpeed;
            }
            else
            {
                PlayerManager.Instance.b_isSprint = false;
            }
            PlayerAnimation.Instance.animator.SetBool("isRun", true);

        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (PlayerManager.Instance.b_isSprint)
    //    {
    //        if (collision.transform.parent != null)
    //        {
    //            if (collision.transform.GetComponentInParent<CustomerStateManager>())
    //            {
    //                CustomerStateManager cus = collision.transform.GetComponentInParent<CustomerStateManager>();
    //                if (cus.s_currentState == cus.s_walkAroundState ||
    //                    cus.s_currentState == cus.s_goOutState)
    //                {
    //                    cus.transform.LookAt(transform.position);
    //                    cus.SwitchState(cus.s_pushState);
    //                    StartCoroutine(StopPlayer());
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (collision.transform.TryGetComponent<CustomerStateManager>(out CustomerStateManager cus))
    //            {
    //                if (cus.s_currentState == cus.s_walkAroundState ||
    //                    cus.s_currentState == cus.s_goOutState)
    //                {
    //                    cus.transform.LookAt(transform.position);
    //                    cus.SwitchState(cus.s_pushState);
    //                    StartCoroutine(StopPlayer());

    //                }
    //            }
    //        }

    //    }
    //}

    //IEnumerator StopPlayer()
    //{
    //    PlayerManager.Instance.b_canMove = false;
    //    yield return new WaitForSeconds(0.5f);
    //    PlayerManager.Instance.b_canMove = true;
    //}

}
