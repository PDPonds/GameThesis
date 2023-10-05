using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : Auto_Singleton<PlayerParticle>, IObserver
{
    [Header("===== MainObserver =====")]
    public MainObserver s_punchTrigger;

    [Header("===== PlayerPunch =====")]
    public GameObject g_hitShockWave;



    private void OnEnable()
    {
        s_punchTrigger.AddObserver(this);
    }

    private void OnDisable()
    {
        s_punchTrigger.RemoveObserver(this);
    }

    public void FuncToDo(ActionObserver action)
    {
        switch (action)
        {
            case ActionObserver.PlayerAttackHit:

                StartCoroutine(ActiveAndDisableGameObject(g_hitShockWave, .5f, PlayerManager.Instance.v_punchHitPoint));

                break;

            default: break;
        }
    }

    IEnumerator ActiveAndDisableGameObject(GameObject obj, float time, Vector3 point)
    {
        obj.transform.position = point;
        obj.SetActive(true);
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

}
