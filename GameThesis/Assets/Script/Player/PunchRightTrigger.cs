using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchRightTrigger : MainObserver
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageAble = other.GetComponentInParent<IDamageable>();
            if (damageAble != null)
            {
                if(PlayerAnimation.Instance.animator.GetCurrentAnimatorStateInfo(0).IsName("HeavyPunch"))
                {
                    damageAble.TakeDamage(2);
                }
                else
                {
                    damageAble.TakeDamage(1);

                }

                if (transform.TryGetComponent(out Collider collider))
                {
                    if (collider.enabled == true)
                    {
                        collider.enabled = false;
                    }
                }

                Vector3 hitpoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                PlayerManager.Instance.v_punchHitPoint = hitpoint;
                ActiveAllObserver(ActionObserver.PlayerAttackRightHit);
            }
        }
    }

}
