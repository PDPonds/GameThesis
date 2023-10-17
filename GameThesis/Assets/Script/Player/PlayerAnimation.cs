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
                StartCoroutine(cantMovePlayer());
                break;
            case ActionObserver.PlayerLeftSoftPunch:

                animator.Play("LeftPunch");
                StartCoroutine(cantMovePlayer());

                break;
            case ActionObserver.PlayerRightSoftPunch:

                animator.Play("RightPunch");
                StartCoroutine(cantMovePlayer());

                break;
            case ActionObserver.PlayerGuard:

                animator.SetBool("isGuard", PlayerManager.Instance.b_isGuard);

                break;
            case ActionObserver.PlayerDead:



                break;
            default: break;
        }
    }

    IEnumerator cantMovePlayer()
    {
        PlayerManager.Instance.b_canMove = false;
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.b_canMove = true;

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
