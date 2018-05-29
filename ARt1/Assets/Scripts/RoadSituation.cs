using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class RoadSituation
{
    public RoadSituation(int countOfCars, PositionRotation[] posRotAnimCar, bool vIP, Direction[] directions,
        TrafficSign trafficSign, int coutOfSigns, PositionRotation[] posRotSign,
        TrafficLight trafficLight, int coutOfLights, PositionRotation[] posRotLight)
    {
        CountOfCars = countOfCars;
        this.posRotAnimCar = posRotAnimCar;
        VIP = vIP;
        this.directions = directions;
        this.trafficSign = trafficSign;
        CoutOfSigns = coutOfSigns;
        this.posRotSign = posRotSign;
        this.trafficLight = trafficLight;
        CoutOfLights = coutOfLights;
        this.posRotLight = posRotLight;
    }

    public int CountOfCars { get; private set; }
    public PositionRotation[] posRotAnimCar { get; private set; }
    public bool VIP { get; private set; }
    public Direction[] directions { get; private set; }

    public TrafficSign trafficSign { get; private set; }
    public int CoutOfSigns { get; private set; }
    public PositionRotation[] posRotSign { get; private set; }

    public TrafficLight trafficLight { get; private set; }
    public int CoutOfLights { get; private set; }
    public PositionRotation[] posRotLight { get; private set; }
}

