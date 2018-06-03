using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Timer
{
    public float TimeCount { get; private set; }
    public bool IsActive { get; private set; }
    public Action CallBack { get; private set; }

    public Timer(float time, Action callback)
    {
        TimeCount = time;
        CallBack += callback;
        IsActive = true;
    }
    public void Execute(float deltaTime)
    {
        TimeCount -= deltaTime;
        if (TimeCount<0)
        {
            Stop();
            CallBack();
        }
    }
    public void Stop()
    {
        IsActive = false;
    }
}

