﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
using UnityEngine;

class LeftTL : PriorityTL
{
    public override void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        
        Car observeCar;

        base.SetPriority(comperative, settingCar);
        

        if (comperative.TryGetValue(ComperativeLocation.Front, out observeCar))
        {
            if (observeCar.Direction != Direction.left)
            {
                settingCar.priority++;
            }
        }


    }
}

