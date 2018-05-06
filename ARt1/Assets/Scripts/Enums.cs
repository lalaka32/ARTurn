﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    public enum TrafficLight : byte { off = 0, red, green }
    public enum Direction : byte { forward = 0, right, left  }
    public enum Priority : byte { first = 0, second, third, fourth }
    public enum Position :sbyte { first, second, third, fourth }
}
