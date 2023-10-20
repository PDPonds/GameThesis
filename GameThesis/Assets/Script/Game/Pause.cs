using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPause = false;

    private void Awake()
    {
        isPause = true;
    }

    private void Update()
    {
        if (isPause)
        {
            if (!UIManager.Instance.g_tutorial.activeSelf)
                UIManager.Instance.g_tutorial.SetActive(true);

            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            if (UIManager.Instance.g_tutorial.activeSelf)
                UIManager.Instance.g_tutorial.SetActive(false);

            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
