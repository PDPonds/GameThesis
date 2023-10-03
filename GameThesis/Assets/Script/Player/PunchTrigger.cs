using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<AIController>())
        {
            AIController ai = other.GetComponentInParent<AIController>();
            switch (ai.type)
            {
                case AIType.Customer:

                    switch (ai.state)
                    {
                        case AIState.Activity:
                            ai.state = AIState.Fight;
                            break;
                        default: break;
                    }

                    break;
                case AIType.Employee:

                    switch (ai.state)
                    {
                        case AIState.Activity:
                            ai.state = AIState.Fight;
                            break;
                        default: break;
                    }
                    break;

                case AIType.Animal:

                    switch (ai.state)
                    {
                        case AIState.Activity:
                            ai.state = AIState.Escape;
                            break;
                        default: break;
                    }

                    break;
                default: break;
            }

            ai.TakeDamege();
            PlayerManager.Instance.c_punchCol.enabled = false;
        }
    }
}
