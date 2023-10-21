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

    [Header("===== Throng Customer =====")]
    public Transform t_thongCenterPoint;
    public float f_throngDistance;
    public float f_throngArea;

    private void Awake()
    {
        s_gameState = GetComponent<GameState>();
        f_coin = f_startCoin;
    }

    public void AddCoin(float amount)
    {
        if (amount < 0) amount = amount * -1;

        f_coin += amount;
        Debug.Log(amount+" $");

        moneyTracker += amount;
        if(moneyTracker > 200)
        {
            Debug.Log("Collected a total of 200$");
            money_objective.SetActive(false);
            money_objective_done.SetActive(true);
        }
    }

    public void RemoveCoin(float amount)
    {
        f_coin -= amount;
    }

    public void EnableThrong(Vector3 worldPos)
    {
        t_thongCenterPoint.transform.position = worldPos;
        ActiveThrong at = t_thongCenterPoint.GetComponent<ActiveThrong>();
        at.enabled = true;
    }

    public void DisableThrong()
    {
        ActiveThrong at = t_thongCenterPoint.GetComponent<ActiveThrong>();
        at.enabled = false;
    }

    public bool isThrongEnable()
    {
        ActiveThrong at = t_thongCenterPoint.GetComponent<ActiveThrong>();
        return at.enabled;
    }

}
