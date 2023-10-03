using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Auto_Singleton<PlayerAnimation>, IObserver
{
    public MainObserver s_fistCombat;
    public void FuncToDo(ActionObserver action)
    {
        switch (action)
        {
            case ActionObserver.PlayerHoldPunch:

                break;
            case ActionObserver.PlayerPunch:

                break;
            default: break;
        }
    }

    private void OnEnable()
    {
        s_fistCombat.AddObserver(this);
    }

    private void OnDisable()
    {
        s_fistCombat.RemoveObserver(this);
    }

}
