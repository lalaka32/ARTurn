using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeManager", menuName = "Managers/TimeManager")]
class TimerManager : ManagerBase
{
    List<Timer> times = new List<Timer>();

    public Timer SetTimer(float time,Action callback)
    {
        Timer temp = new Timer(time, callback);
        times.Add(temp);
        return temp;
    }
    void deleteUnActiveTimers()
    {
        for (int i = 0; i < times.Count; i++)
        {
            if (times[i].IsActive == false)
            {
                times.Remove(times[i]);
            }
        }
    }
    public void ProsessingTimer(float deltatime)
    {
        deleteUnActiveTimers();
        for (int i = 0; i < times.Count; i++)
        {
            times[i].Execute(deltatime);
        }
    }
}

