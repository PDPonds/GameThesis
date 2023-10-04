using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(StateManager ai);
    public abstract void UpdateState(StateManager ai);

}
