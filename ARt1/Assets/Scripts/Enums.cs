using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    public enum TrafficLight : byte { off = 0, red, green,empty }
    public enum TrafficSign  { main, secondary, empty }//stop
    public enum Direction : byte { right = 0,forward , left  }
    public enum Priority : byte { first = 0, second, third, fourth }
    public enum Position :sbyte { first, second, third, fourth }
    public enum ComperativeLocation { Right, Front, Left }
}
