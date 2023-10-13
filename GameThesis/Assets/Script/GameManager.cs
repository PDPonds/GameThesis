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

    private void Awake()
    {
        s_gameState = GetComponent<GameState>();
    }

    public void AddCoin(float amount)
    {
        f_coin += amount;
    }

    public void RemoveCoint(float amount)
    {
        f_coin -= amount;
    }

}
