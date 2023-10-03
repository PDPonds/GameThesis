using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Auto_Singleton<PlayerAnimation>, IObserver
{
    public MainObserver s_fistCombat;

    Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void FuncToDo(ActionObserver action)
    {
        switch (action)
        {
            case ActionObserver.PlayerHoldPunch:

                animator.SetBool("isHold", PlayerManager.Instance.b_isHold);

                break;
            case ActionObserver.PlayerPunch:

                animator.SetBool("isHold", false);
                animator.Play("Punch");

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
