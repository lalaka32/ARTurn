using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
using UnityEngine;

class UnequalBase : IDirectionitatible
{
    public void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        Car observeCar;
        TrafficSign trafficSign = ToolBox.Get<SignManager>().TS[settingCar.Position];
        TrafficSign finalSign = ToolBox.Get<SignManager>().TS[settingCar.FinalPosition];

        if (trafficSign == TrafficSign.secondary)
        {

        }
    }
}

