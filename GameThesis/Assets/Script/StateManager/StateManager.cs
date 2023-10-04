using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    public abstract BaseState s_currentState { get; set; }

    public void SwitchState(BaseState state)
    {
        s_currentState = state;
        s_currentState.EnterState(this);
    }


}
