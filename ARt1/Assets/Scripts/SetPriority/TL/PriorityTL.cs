using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PriorityTL : IDirectionitatible
{
    public virtual void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        TrafficLight trafficSignal = ToolBox.Get<TrafficLightManager>().PosTL[settingCar.Position];

        Car observeCarLeft;
        Car observeCarRight;


        if (trafficSignal == TrafficLight.Red)
        {
            if (comperative.ContainsKey(ComperativeLocation.Left) || comperative.ContainsKey(ComperativeLocation.Right))
            {
                settingCar.priority++;
            }

            if (comperative.TryGetValue(ComperativeLocation.Left, out observeCarLeft) && comperative.TryGetValue(ComperativeLocation.Right, out observeCarRight))
            {
                if (observeCarLeft.Direction == Direction.Left && observeCarRight.Direction != Direction.Left)
                {
                    settingCar.priority++;
                }
                else if (observeCarRight.Direction == Direction.Left && observeCarLeft.Direction != Direction.Left)
                {
                    settingCar.priority++;
                }

            }

        }

    }
}

