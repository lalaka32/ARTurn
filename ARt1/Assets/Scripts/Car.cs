﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Car : MonoBehaviour
{
    public Priority priority;

    private Position _position;
    public bool isstop = false;

    [SerializeField]
    Direction direction;

    public Position Position
    {
        get { return _position; }
        set
        {
            if ((sbyte)value < 0)
            {
                _position = 4 + value;
            }
            else if ((sbyte)value >= 4)
            {
                _position = value - 4;
            }
            else
            {
                _position = value;
            }
        }
    }

    public Direction Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }

    }
    public Position FinalPosition
    {
        get
        {
            Position fin = (Position)((int)Position - (int)Direction-1);
            while (fin < 0 || fin >= (Position)4)
            {
                if ((sbyte)fin < 0)
                {
                    fin += 4;
                }
                else if ((sbyte)fin >= 4)
                {
                    fin -= 4;
                }
            }
            return fin;
        }
    }

    public virtual void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        IDirectionitatible carDir;
        if (ToolBox.Get<TrafficLightManager>().PosTL == null && ToolBox.Get<SignManager>().TS == null)
        {
            switch (Direction)
            {
                case Direction.Forward:
                    carDir = new ForwardQvalent();
                    carDir.SetPriority(comperative, settingCar);
                    break;
                case Direction.Right:
                    carDir = new DirectionRight();
                    carDir.SetPriority(comperative, settingCar);
                    break;
                case Direction.Left:
                    carDir = new LeftDirectionQvalent();
                    carDir.SetPriority(comperative, settingCar);
                    break;
            }
        }
        else if (ToolBox.Get<TrafficLightManager>().PosTL != null)
        {
            switch (Direction)
            {
                case Direction.Forward:
                    carDir = new PriorityTL();
                    carDir.SetPriority(comperative, settingCar);
                    break;
                case Direction.Right:
                    carDir = new PriorityTL();
                    carDir.SetPriority(comperative, settingCar);
                    break;
                case Direction.Left:
                    carDir = new LeftTL();
                    carDir.SetPriority(comperative, settingCar);
                    break;
            }
        }
        else if (ToolBox.Get<SignManager>().TS != null)
        {
            switch (Direction)
            {
                case Direction.Forward:
                    carDir = new Unqvalent();
                    carDir.SetPriority(comperative, settingCar);
                    break;
                case Direction.Right:
                    carDir = new Unqvalent();
                    carDir.SetPriority(comperative, settingCar);
                    break;
                case Direction.Left:
                    carDir = new LeftUnqvalent();
                    carDir.SetPriority(comperative, settingCar);
                    break;
            }
        }
    }

    public void StartAnime()
    {
        var animator = GetComponent<Animator>();
        switch (Direction)
        {
            case Direction.Forward:
                GetComponentInChildren<Animator>().Play("CarForwardanim");
                break;
            case Direction.Left:
                GetComponentInChildren<Animator>().Play("CarLeftanim");
                break;
            case Direction.Right:
                GetComponentInChildren<Animator>().Play("CarRightanim");
                break;
        }
    }
}
