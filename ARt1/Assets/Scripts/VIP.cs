﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;

class VIP:Car
{
    private void Awake()
    {
        //СДелать мигалки
    }
    public override void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        foreach (var item in comperative)
        {
            item.Value.priority++;
        }
    }
}

