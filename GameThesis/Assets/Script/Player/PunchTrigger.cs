using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MainObserver
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageAble = other.GetComponentInParent<IDamageable>();
            if (damageAble != null)
            {
                damageAble.TakeDamage(1);
                //PlayerManager.Instance.c_punchCol.enabled = false;
                ActiveAllObserver(ActionObserver.PlayerAttackHit);
            }
        }
    }

}
