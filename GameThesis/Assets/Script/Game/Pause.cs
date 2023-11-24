using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPause = false;

    private void Update()
    {

        if (isPause)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;

            if (GameManager.Instance.s_gameState.s_currentState ==
                GameManager.Instance.s_gameState.s_afterOpenState)
            {
                if (RestaurantManager.Instance.RestaurantIsEmpty() ||
                    DebugController.Instance.showConsole)
                {
                    if (UIManager.Instance.g_summary.activeSelf)
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                    else
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }

            }
            else
            {
                if (DebugController.Instance.showConsole ||
                        UIManager.Instance.winPage.activeSelf ||
                        UIManager.Instance.losePage.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }

            }

        }
    }

}
