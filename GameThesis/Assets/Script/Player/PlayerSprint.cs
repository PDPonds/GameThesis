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

    private void OnCollisionEnter(Collision collision)
    {
        if (PlayerManager.Instance.b_isSprint)
        {
            if (collision.transform.parent != null)
            {
                if (collision.transform.GetComponentInParent<CustomerStateManager>())
                {
                    CustomerStateManager customerStateManager = collision.transform.GetComponentInParent<CustomerStateManager>();
                    if (customerStateManager.s_currentState == customerStateManager.s_walkAroundState ||
                        customerStateManager.s_currentState == customerStateManager.s_goOutState)
                    {
                        customerStateManager.transform.LookAt(transform.position);
                        customerStateManager.SwitchState(customerStateManager.s_pushState);
                        StartCoroutine(StopPlayer());
                    }
                }
            }
            else
            {
                if (collision.transform.TryGetComponent<CustomerStateManager>(out CustomerStateManager customerStateManager))
                {
                    if (customerStateManager.s_currentState == customerStateManager.s_walkAroundState ||
                        customerStateManager.s_currentState == customerStateManager.s_goOutState)
                    {
                        customerStateManager.transform.LookAt(transform.position);
                        customerStateManager.SwitchState(customerStateManager.s_pushState);
                        StartCoroutine(StopPlayer());

                    }
                }
            }

        }
    }

    IEnumerator StopPlayer()
    {
        PlayerManager.Instance.b_canMove = false;
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.b_canMove = true;
    }

}
