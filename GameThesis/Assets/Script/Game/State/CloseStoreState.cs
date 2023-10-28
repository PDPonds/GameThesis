using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStoreState : BaseState
{
    public override void EnterState(StateManager state)
    {
        GameState gameState = (GameState)state;
    }

    public override void UpdateState(StateManager state)
    {
        GameState gameState = (GameState)state;

    }
}
