using UnityEngine;

public abstract class AIBaseState
{
    public abstract void EnterState(AIStateManager ai);
    public abstract void UpdateState(AIStateManager ai);

}
