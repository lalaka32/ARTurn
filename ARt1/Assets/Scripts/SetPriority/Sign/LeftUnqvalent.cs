using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;

class LeftUnqvalent : Unqvalent
{
    public override void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        Car observeCar;

        base.SetPriority(comperative, settingCar);

        if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
        {
            if (observeCar.Direction != Direction.Left)
            {
                settingCar.priority++;
            }
        }
    }
}

