using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("HitEnemy");
            IDamageable damageAble = other.GetComponentInParent<IDamageable>();
            if (damageAble != null)
            {
                Debug.Log("EnemyTakeDamage");
                damageAble.TakeDamage(1);
                PlayerManager.Instance.c_punchCol.enabled = false;
            }
        }
    }
}
