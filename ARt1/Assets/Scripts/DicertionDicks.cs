using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
using UnityEngine;

class DicertionDicks : IDirectionitatible
{
    public void SetPriority(Dictionary<Position, Car> listOfPositions, Position positionSetting)
    {
        Car settingCar;
        Car observeCar;

        Dictionary<ComperativeLocation, Car> dicWithСomparative = GetComperative(listOfPositions, positionSetting, out settingCar);

        if (dicWithСomparative.TryGetValue(ComperativeLocation.Right, out observeCar))
        {
            if (observeCar.Direction == Direction.forward)
            {
                settingCar.priority++;

                if (dicWithСomparative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
            }

            else if (observeCar.Direction == Direction.left)
            {
                settingCar.priority++;
                if (dicWithСomparative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
                else
                {
                    if (dicWithСomparative.TryGetValue(ComperativeLocation.Front, out observeCar))
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
    static Dictionary<ComperativeLocation, Car> GetComperative(Dictionary<Position, Car> listOfPositions, Position positionSetting, out Car settingCar)
    {
        settingCar = listOfPositions[positionSetting];
        Car observeCar;
        Dictionary<ComperativeLocation, Car> dicWithСomparative = new Dictionary<ComperativeLocation, Car>();

        for (int i = 0; i < 3; i++)
        {
            settingCar.Position--;
            if (listOfPositions.TryGetValue(settingCar.Position, out observeCar))
            {
                dicWithСomparative.Add((ComperativeLocation)i, observeCar);
            }
        }
        settingCar.Position--;

        return dicWithСomparative;
    }
}
class LeftUpdate : IDirectionitatible
{
    static Dictionary<ComperativeLocation, Car> GetComperative(Dictionary<Position, Car> listOfPositions, Position positionSetting, out Car settingCar)
    {
        settingCar = listOfPositions[positionSetting];
        Car observeCar;
        Dictionary<ComperativeLocation, Car> dicWithСomparative = new Dictionary<ComperativeLocation, Car>();

        for (int i = 0; i < 3; i++)
        {
            settingCar.Position--;
            if (listOfPositions.TryGetValue(settingCar.Position, out observeCar))
            {
                dicWithСomparative.Add((ComperativeLocation)i, observeCar);
            }
        }
        settingCar.Position--;

        return dicWithСomparative;
    }
    public void SetPriority(Dictionary<Position, Car> listOfPositions, Position positionSetting)
    {
        Car settingCar;
        Car observeCar;

        Dictionary<ComperativeLocation, Car> dicWithСomparative = GetComperative(listOfPositions, positionSetting, out settingCar);

        if (dicWithСomparative.TryGetValue(ComperativeLocation.Right, out observeCar))
        {
            settingCar.priority++;
            if (observeCar.Direction == Direction.forward)
            {
                if (dicWithСomparative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
            }
            else if (observeCar.Direction == Direction.left)
            {
                if (dicWithСomparative.TryGetValue(ComperativeLocation.Front, out observeCar))
                {
                    settingCar.priority++;
                }
                else
                {
                    if (dicWithСomparative.TryGetValue(ComperativeLocation.Left, out observeCar))
                    {
                        if (observeCar.Direction== Direction.right)
                        {
                            settingCar.priority++;
                        }
                    }
                }
            }
        }
        else if (dicWithСomparative.TryGetValue(ComperativeLocation.Front, out observeCar))
        {
            if (observeCar.Direction == Direction.right)
            {
                settingCar.priority++;
            }
            else if (observeCar.Direction == Direction.forward)
            {
                if (!dicWithСomparative.TryGetValue(ComperativeLocation.Left, out observeCar))
                {
                    settingCar.priority++;
                }
            }
        }
        Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
    }
}