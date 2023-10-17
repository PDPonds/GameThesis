using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : Auto_Singleton<TimeController>
{
    public float f_timeMultiplier;

    public float f_startTime;
    public float f_endTime;

    public DateTime d_currentTime;

    private void Start()
    {
        d_currentTime = DateTime.Now.Date + TimeSpan.FromHours(f_startTime);
    }

    void Update()
    {
        d_currentTime = d_currentTime.AddSeconds(Time.deltaTime * f_timeMultiplier);
    }

}
