using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
using UnityEngine;

class ForwardDirection : IDirectionitatible
{
    public void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        Car observeCar;

        if (comperative.TryGetValue(ComperativeLocation.Right, out observeCar))
        {
            if (observeCar.Direction == Direction.forward)
            {
                settingCar.priority++;

                if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
            }

            else if (observeCar.Direction == Direction.left)
            {
                settingCar.priority++;
                if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
                else
                {
                    if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
                    {
                        if (observeCar.Direction == Direction.right)
                        {
                            settingCar.priority++;
                        }
                    }
                }
            }

            else if (observeCar.Direction == Direction.right)
            {
                settingCar.priority++;
            }
        }
        Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
    } 
}
class LeftDirection : IDirectionitatible
{
    public void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        Car observeCar;

        if (comperative.TryGetValue(ComperativeLocation.Right, out observeCar))
        {
            settingCar.priority++;
            if (observeCar.Direction == Direction.forward)
            {
                if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
            }
            else if (observeCar.Direction == Direction.left)
            {
                if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
                else
                {
                    if (comperative.TryGetValue(ComperativeLocation.Left, out observeCar))
                    {
                        if (observeCar.Direction== Direction.right)
                        {
                            settingCar.priority++;
                        }
                    }
                }
            }
        }
        else if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
        {
            if (observeCar.Direction == Direction.right)
            {
                settingCar.priority++;
            }
            else if (observeCar.Direction == Direction.forward)
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