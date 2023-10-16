using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{

    void Update()
    {
        if (!PlayerManager.Instance.b_isSprint)
            PlayerManager.Instance.f_moveSpeed = PlayerManager.Instance.f_walkSpeed;
        else
            PlayerManager.Instance.f_moveSpeed = PlayerManager.Instance.f_runSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PlayerManager.Instance.b_isSprint)
        {
            if(collision.transform.parent != null)
            {
                if (collision.transform.GetComponentInParent<CustomerStateManager>())
                {
                    CustomerStateManager customerStateManager = collision.transform.GetComponentInParent<CustomerStateManager>();
                    customerStateManager.SwitchState(customerStateManager.s_fightState);
                    StartCoroutine(AIPullPlayer());
                }
            }
            else
            {
                if (collision.transform.TryGetComponent<CustomerStateManager>(out CustomerStateManager customerStateManager))
                {
                    customerStateManager.SwitchState(customerStateManager.s_fightState);
                    StartCoroutine(AIPullPlayer());
                }
            }
            
        }
    }

    IEnumerator AIPullPlayer()
    {
        PlayerManager.Instance.b_canMove = false;
        yield return new WaitForSeconds(1);
        PlayerManager.Instance.b_canMove = true;
    }

}
