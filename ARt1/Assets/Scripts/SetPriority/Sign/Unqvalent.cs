using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;

class Unqvalent : IDirectionitatible
{
    public virtual void  SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        TrafficSign trafficSignal = ToolBox.Get<SignManager>().TS[settingCar.Position];

        Car observeCarLeft;
        Car observeCarRight;

        if (trafficSignal == TrafficSign.secondary)
        {
            if (comperative.ContainsKey(ComperativeLocation.Left) || comperative.ContainsKey(ComperativeLocation.Right))
            {
                settingCar.priority++;
            }

            if (comperative.TryGetValue(ComperativeLocation.Left, out observeCarLeft) && comperative.TryGetValue(ComperativeLocation.Right, out observeCarRight))
            {
                if (observeCarLeft.Direction == Direction.left && observeCarRight.Direction != Direction.left)
                {
                    settingCar.priority++;
                }
                else if (observeCarRight.Direction == Direction.left && observeCarLeft.Direction != Direction.left)
                {
                    settingCar.priority++;
                }

            }

        }
    }
}

