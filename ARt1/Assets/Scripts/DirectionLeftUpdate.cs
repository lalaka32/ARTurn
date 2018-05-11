using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//ctrl k d
class DirectionLeftUpdate : IDirectionitatible
{
    public void SetPriority(Dictionary<Position, Car> listOfPositions, Position positionSetting)
    {
        Car settingCar = listOfPositions[positionSetting];
        Car observeCar;

        listOfPositions.Remove(positionSetting);
        settingCar.Position--;
        if (listOfPositions.TryGetValue(settingCar.Position, out observeCar))
        {
            if (observeCar.Direction == Direction.forward)
            {
                settingCar.priority++;
                settingCar.Position--;
                if (listOfPositions.TryGetValue(settingCar.Position, out observeCar))
                {
                    settingCar.priority++;
                }
                settingCar.Position += 2;
                Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
            }
            else if (observeCar.Direction == Direction.left)
            {
                settingCar.priority++;
                settingCar.Position--;
                if (listOfPositions.TryGetValue(settingCar.Position, out observeCar))
                {
                    settingCar.priority++;
                    settingCar.Position += 2;
                    Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
                }
                else//Не уверен
                {
                    settingCar.Position--;
                    if (listOfPositions.TryGetValue(settingCar.Position, out observeCar))
                    {
                        settingCar.priority++;
                    }
                    settingCar.Position += 3;
                    Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
                }
            }
            else if (observeCar.Direction == Direction.right)
            {
                settingCar.priority++;
                settingCar.Position++;
                Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
            }
        }
        else
        {
            settingCar.Position--;
            if (listOfPositions.TryGetValue(settingCar.Position, out observeCar))
            {
                if (observeCar.Direction == Direction.right)
                {
                    settingCar.priority++;
                }
                else if (observeCar.Direction == Direction.forward)
                {
                    settingCar.Position--;
                    if (!listOfPositions.TryGetValue(settingCar.Position, out observeCar))
                    {
                        settingCar.priority++;
                    }
                    settingCar.Position++;
                }
            }
            settingCar.Position += 2;
            listOfPositions.Add(settingCar.Position, settingCar);
            Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
        }
    }
}

