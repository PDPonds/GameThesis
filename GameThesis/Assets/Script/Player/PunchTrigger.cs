using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageAble = other.GetComponentInParent<IDamageable>();
            if (damageAble != null)
            {
                damageAble.TakeDamage(1);
                PlayerManager.Instance.c_punchCol.enabled = false;
                StartCoroutine(pauseAnim());
            }
        }
    }

    IEnumerator pauseAnim()
    {
        PlayerAnimation.Instance.animator.SetBool("isAtk", false);
        PlayerAnimation.Instance.animator.SetBool("isHold", false);
        PlayerAnimation.Instance.animator.speed = 0;
        yield return new WaitForSeconds(.2f);
        PlayerAnimation.Instance.animator.speed = 1;
    }

}
