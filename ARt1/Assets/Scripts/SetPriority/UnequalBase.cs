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
        TrafficLight trafficSignal = ToolBox.Get<TrafficLightManager>().PosTL[settingCar.Position];
        Debug.Log(trafficSignal + "    " + settingCar.Position);
        if (ToolBox.Get<SignManager>().TS[settingCar.Position] == TrafficSign.main)
        {
            if (settingCar.Direction != Direction.right)
            {
                if (comperative.TryGetValue(ComperativeLocation.Right, out observeCar))
                {
                    if (ToolBox.Get<SignManager>().TS[observeCar.Position] == TrafficSign.main)
                    {
                        settingCar.priority++;
                    }
                }
                if (settingCar.Direction == Direction.left)//мб лучше разбить на классы
                                                           //т.к. если машина не поворачивает то бессмысленная проверка
                {
                    if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
                    {
                        if (ToolBox.Get<SignManager>().TS[observeCar.Position] == TrafficSign.main)
                        {
                            if (observeCar.Direction != Direction.left)
                            {
                                settingCar.priority++;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            //короче пока хз как всё проверать
            //наверно нужно просто типо finalPosition
            //чтобы не искать всё
            if (comperative.TryGetValue(ComperativeLocation.Right, out observeCar))
            {
                if (ToolBox.Get<SignManager>().TS[observeCar.Position] == TrafficSign.main)
                {
                    settingCar.priority++;
                }

            }

        }
    }
}

