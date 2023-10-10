using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : StateManager
{
    public override BaseState s_currentState { get; set; }

    public BeforeStoreOpenState beforeState = new BeforeStoreOpenState();
    public OpenStoreState openState = new OpenStoreState();
    public SummaryDayState summaryState = new SummaryDayState();

    private void Start() {

        SwitchState(beforeState);

    }
    
    private void Update() {

        s_currentState.UpdateState(this);

    }

}
