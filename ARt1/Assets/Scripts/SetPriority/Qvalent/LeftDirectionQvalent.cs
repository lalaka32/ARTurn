using System.Collections.Generic;
using Enums;
using UnityEngine;

class LeftDirectionQvalent : IDirectionitatible
{
    public void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        Car observeCar;

        if (comperative.TryGetValue(ComperativeLocation.Right, out observeCar))
        {
            settingCar.priority++;
            if (observeCar.Direction == Direction.Forward)
            {
                if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
            }
            else if (observeCar.Direction == Direction.Left)
            {
                if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
                else
                {
                    if (comperative.TryGetValue(ComperativeLocation.Left, out observeCar))
                    {
                        if (observeCar.Direction== Direction.Right)
                        {
                            settingCar.priority++;
                        }
                    }
                }
            }
        }
        else if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
        {
            if (observeCar.Direction == Direction.Right)
            {
                settingCar.priority++;
            }
            else if (observeCar.Direction == Direction.Forward)
            {
                if (!comperative.TryGetValue(ComperativeLocation.Left, out observeCar))
                {
                    settingCar.priority++;
                }
            }
        }
        Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
    }
}