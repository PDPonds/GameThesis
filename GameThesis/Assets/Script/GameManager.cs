using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : Auto_Singleton<GameManager>
{
    public LayerMask lm_playerMask;
    public LayerMask lm_enemyMask;

    public Transform t_restaurantForntDoor;

    [HideInInspector] public GameState s_gameState;

    private void Awake()
    {
        s_gameState = GetComponent<GameState>();
    }

}
