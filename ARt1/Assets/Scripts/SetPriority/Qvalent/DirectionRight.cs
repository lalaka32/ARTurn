using System.Collections.Generic;
using UnityEngine;
using Enums;

public class DirectionRight : IDirectionitatible
{
    public void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        settingCar.priority = Priority.first;

        Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
    }
}