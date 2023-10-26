using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : Auto_Singleton<GameManager>
{
    public GameObject money_objective;
    public GameObject money_objective_done;

    public LayerMask lm_playerMask;
    public LayerMask lm_enemyMask;

    public Transform t_restaurantForntDoor;
    public Transform t_counterPos;
    public Transform t_getFoodPos;
    public Transform t_stayPos;

    public float f_coin;
    public float f_startCoin;
    private float moneyTracker = 0;

    [HideInInspector] public GameState s_gameState;

    [Header("===== Crowd Customer =====")]
    public Transform t_crowdCenterPoint;
    public float f_crowdDistance;
    public float f_crowdArea;

    private void Awake()
    {
        s_gameState = GetComponent<GameState>();
        f_coin = f_startCoin;
    }

    public void AddCoin(float amount)
    {
        if (amount < 0) amount = amount * -1;

        f_coin += amount;

        moneyTracker += amount;
        if(moneyTracker > 200)
        {
            money_objective.SetActive(false);
            money_objective_done.SetActive(true);
        }
    }

    public void RemoveCoin(float amount)
    {
        f_coin -= amount;
    }

    public void EnableCrowd(Vector3 worldPos)
    {
        t_crowdCenterPoint.transform.position = worldPos;
        ActiveCrowd at = t_crowdCenterPoint.GetComponent<ActiveCrowd>();
        at.enabled = true;
    }

    public void DisableCrowd()
    {
        ActiveCrowd at = t_crowdCenterPoint.GetComponent<ActiveCrowd>();
        at.enabled = false;
    }

    public bool isCrowdEnable()
    {
        ActiveCrowd at = t_crowdCenterPoint.GetComponent<ActiveCrowd>();
        return at.enabled;
    }

}
