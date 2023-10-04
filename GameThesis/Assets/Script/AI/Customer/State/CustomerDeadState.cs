using UnityEngine;

public class CustomerDeadState : BaseState
{
    public override void EnterState(StateManager ai)
    {

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.RagdollOn();
        customerStateManager.DisablePunch();
    }
}
