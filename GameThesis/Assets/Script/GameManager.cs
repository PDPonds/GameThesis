using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : Auto_Singleton<GameManager>
{
    public LayerMask lm_playerMask;
    public LayerMask lm_enemyMask;

    public Transform t_restaurantForntDoor;
    public Transform t_counterPos;
    public Transform t_getFoodPos;
    public Transform t_stayPos;
    public Transform t_spawnPos;

    [Header("===== Money =====")]
    public float f_pocketMoney;
    public float f_coin;
    public float f_startCoin;

    [HideInInspector] public GameState s_gameState;

    [Header("===== Crowd Customer =====")]
    public Transform t_crowdCenterPoint;
    public float f_crowdDistance;
    public float f_crowdArea;

    [Header("===== Day =====")]
    public int i_startDay;
    [HideInInspector] public int i_currentDay;

    public FrameStop framestop;

    private void Awake()
    {
        s_gameState = GetComponent<GameState>();
        f_coin = f_startCoin;
        i_currentDay = i_startDay;
    }

    public void MovePlayerToSpawnPoint()
    {
        PlayerManager.Instance.transform.position = t_spawnPos.position;
    }

    public void AddCoin(float amount)
    {
        if (amount < 0) return;

        UIManager.Instance.uiAnimCon.AddCashAnim(amount);
        f_coin += amount;
        SoundManager.Instance.PlayAddCoinSound();
    }

    public void RemoveCoin(float amount)
    {
        SoundManager.Instance.PlayRemoveCoinSound();
        UIManager.Instance.uiAnimCon.RemoveCashAnim(amount);
        f_coin -= amount;
    }

    public void AddPocketMoney(float amount)
    {
        if (amount < 0) return;

        UIManager.Instance.uiAnimPocket.AddCashAnim(amount);
        f_pocketMoney += amount;
        SoundManager.Instance.PlayAddCoinSound();
    }

    public void RemovePocketMoney(float amount)
    {
        SoundManager.Instance.PlayRemoveCoinSound();
        UIManager.Instance.uiAnimPocket.RemoveCashAnim(amount);
        f_pocketMoney -= amount;
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

    public void SkipDay()
    {
        if (s_gameState.s_currentState == s_gameState.s_openState)
        {
            s_gameState.SwitchState(s_gameState.s_afterOpenState);
            RestaurantManager.Instance.ClearRestaurant();
        }
    }

}
