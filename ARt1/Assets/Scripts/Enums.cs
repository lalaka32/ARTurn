using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    public enum TrafficLight : byte { Off = 0, Red, Green,Empty }
    public enum TrafficSign  { Main, Secondary, Empty }//stop
    public enum Direction : byte { Right = 0,Forward , Left  }
    public enum Priority : byte { First = 0, Second, Third, Fourth }
    public enum Position :sbyte { First, Second, Third, Fourth }
    public enum ComperativeLocation { Right, Front, Left }
}
