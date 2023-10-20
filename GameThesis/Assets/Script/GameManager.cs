using System.Collections;
using System.Collections.Generic;
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

    [HideInInspector] public GameState s_gameState;

    [Header("===== Throng Customer =====")]
    public Transform t_thongCenterPoint;
    public float f_throngDistance;
    public float f_throngArea;

    private void Awake()
    {
        s_gameState = GetComponent<GameState>();
    }

    public void AddCoin(float amount)
    {
        f_coin += amount;
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
