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
                GameManager.Instance.framestop.ActivateFrameStop();
                damageAble.TakeDamage(1);
                StartCoroutine(puaseAnim());
                if (transform.TryGetComponent(out Collider collider))
                {
                    if (collider.enabled == true)
                    {
                        collider.enabled = false;
                    }
                }

                if (!GameManager.Instance.isCrowdEnable())
                {
                    GameManager.Instance.EnableCrowd(transform.position);
                }

                Vector3 hitpoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                PlayerManager.Instance.v_punchHitPoint = hitpoint;
                ActiveAllObserver(ActionObserver.PlayerAttackRightHit);
            }
        }
    }

    IEnumerator puaseAnim()
    {
        Animator anim = PlayerAnimation.Instance.animator;
        anim.speed = 0;
        yield return new WaitForSeconds(0.1f);
        anim.speed = 1;

    }

}
