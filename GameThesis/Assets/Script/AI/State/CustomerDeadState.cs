using UnityEngine;

public class CustomerDeadState : AIBaseState
{
    public override void EnterState(AIStateManager ai)
    {
        Debug.Log("Customer Dead");
    }

    public override void UpdateState(AIStateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.RagdollOn();
        customerStateManager.DisablePunch();
    }
}
