using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrameStop : MonoBehaviour
{
    [SerializeField] private float theWorldDuration = 1;
    private float counter = 0;
    private bool isTheWorld = false;

    void Update()
    {


        if (isTheWorld)
        {
            if(counter < theWorldDuration) 
            { 
                Time.timeScale = 0; 
                counter += Time.unscaledDeltaTime;
            }
            else { Time.timeScale = 1; isTheWorld = false; }
        }
    }

    public void ActivateFrameStop()
    {
        isTheWorld = !isTheWorld;
        counter = 0;
    }
}
