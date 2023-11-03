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

    public float f_coin;
    public float f_startCoin;

    public TextMeshProUGUI addCash;
    public TextMeshProUGUI removeCash;
    public UI_animmationController uiAnimCon;

    [HideInInspector] public GameState s_gameState;

    [Header("===== Crowd Customer =====")]
    public Transform t_crowdCenterPoint;
    public float f_crowdDistance;
    public float f_crowdArea;

    public FrameStop framestop;

    private void Awake()
    {
        s_gameState = GetComponent<GameState>();
        f_coin = f_startCoin;
    }

    public void AddCoin(float amount)
    {
        uiAnimCon.AddCashAnim();

        if (amount < 0) return;

        f_coin += amount;

    }

    public void RemoveCoin(float amount)
    {
        uiAnimCon.RemoveCashAnim();
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
