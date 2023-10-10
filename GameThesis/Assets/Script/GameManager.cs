using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : Auto_Singleton<GameManager>
{
    public LayerMask lm_playerMask;
    public LayerMask lm_enemyMask;

    [HideInInspector] public GameState s_gameState;

    private void Awake() {
        s_gameState = GetComponent<GameState>();
    }
}
