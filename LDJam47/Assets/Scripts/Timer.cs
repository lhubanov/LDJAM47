using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public void Start( float TimeToCount )
    {
        TimeRemaining = TimeToCount;
        StartTime = Time.time;
        TimerActive = true;
    }
    public void Tick()
    {
        TimeRemaining = TimeRemaining - Time.deltaTime;
    }

    public bool HasFinished()
    {
        return TimeRemaining <= 0;
    }
    public void Reset()
    {
        StartTime = 0;
        TimerActive = false;
        TimeRemaining = 0;
    }
    public bool IsActive()
    {
        return TimerActive;
    }

    private float StartTime = 0;
    public float TimeRemaining = 0;
    private bool TimerActive = false;
}

