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

    [SerializeField] private Light sun;
    [SerializeField] private float sunRiseHour;
    [SerializeField] private float sunSetHour;
    private TimeSpan sunRiseTime;
    private TimeSpan sunSetTime;

    [SerializeField] private Color dayAmbient;
    [SerializeField] private Color nightAmbient;
    [SerializeField] private AnimationCurve ambientCurve;
    [SerializeField] private float maxSunIntensity;

    private void Start()
    {
        d_currentTime = DateTime.Now.Date + TimeSpan.FromHours(f_startTime);

        sunRiseTime = TimeSpan.FromHours(sunRiseHour);
        sunSetTime = TimeSpan.FromHours(sunSetHour);
    }

    void Update()
    {
        d_currentTime = d_currentTime.AddSeconds(Time.deltaTime * f_timeMultiplier);

        RotateSun();
    }

    private void RotateSun()
    {
        float sunRotationX = 0;

        if (d_currentTime.TimeOfDay > sunRiseTime && d_currentTime.TimeOfDay < sunSetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalTimeDiff(sunRiseTime, sunSetTime);
            TimeSpan timeSinceSunrise = CalTimeDiff(sunRiseTime, d_currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunRotationX = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalTimeDiff(sunSetTime, sunRiseTime);
            TimeSpan timeSinceSunset = CalTimeDiff(sunSetTime, d_currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunRotationX = Mathf.Lerp(180, 360, (float)percentage);
        }

        sun.transform.rotation = Quaternion.Euler(new Vector3(sunRotationX, 0, 0));
    }

    private TimeSpan CalTimeDiff(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan diff = toTime - fromTime;
        if(diff.TotalSeconds < 0)
        {
            diff += TimeSpan.FromHours(24);
        }
        return diff;
    }
}
