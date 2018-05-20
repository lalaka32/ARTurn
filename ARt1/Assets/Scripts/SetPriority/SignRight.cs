using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;

class SignRight : IDirectionitatible
{
    public void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        Dictionary<Position, TrafficSign> trafficSign = ToolBox.Get<SignManager>().TS;
        Car observeCar;
        TrafficSign observeSign = trafficSign[settingCar.Position];

        if (observeSign == TrafficSign.main)
        {
            //походу нужен дикшенери с конечным местоположением
            //Dictionary<Car, Position> finalTrip; не тут конечно
        }

    }
}

