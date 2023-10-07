using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Auto_Singleton<PlayerAnimation>, IObserver
{
    public MainObserver s_fistCombat;
    public MainObserver s_playerGuard;

    [HideInInspector] public Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void FuncToDo(ActionObserver action)
    {
        switch (action)
        {
            case ActionObserver.PlayerHeavyPunch:

                animator.Play("HeavyPunch");

                break;
            case ActionObserver.PlayerLeftSoftPunch:

                animator.Play("LeftPunch");

                break;
            case ActionObserver.PlayerRightSoftPunch:

                animator.Play("RightPunch");

                break;
            case ActionObserver.PlayerGuard:

                animator.SetBool("isGuard", PlayerManager.Instance.b_isGuard);

                break;
            case ActionObserver.PlayerDead:



                break;
            default: break;
        }
    }



    private void OnEnable()
    {
        s_fistCombat.AddObserver(this);
        s_playerGuard.AddObserver(this);
    }

    private void OnDisable()
    {
        s_fistCombat.RemoveObserver(this);
        s_playerGuard.RemoveObserver(this);
    }

}
