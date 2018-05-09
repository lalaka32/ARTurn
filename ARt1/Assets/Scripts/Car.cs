using System.Collections;
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

    public void SetPriority(Car[] cars, int index)
    {
        IDirectionitatible carDir;
        switch (Direction)
        {
            case Direction.forward:
                carDir = new DirectionForward();
                carDir.SetPriority(cars,index);
                break;
            case Direction.right:
                carDir = new DirectionRight();
                carDir.SetPriority(cars, index);
                break;
            case Direction.left:
                carDir = new DirectionLeft();
                carDir.SetPriority(cars, index);
                break;
        }
    }
    private void Awake()
    {
        direction = (Direction)Random.Range(0, 3);
    }
    
    public void StartAnime()
    {
        var animator = GetComponent<Animator>();
        switch (Direction)
        {
            case Direction.forward:
                GetComponentInChildren<Animator>().Play("CarForwardanim");
                break;
            case Direction.left:
                GetComponentInChildren<Animator>().Play("CarLeftanim");
                break;
            case Direction.right:
                GetComponentInChildren<Animator>().Play("CarRightanim");
                break;
        }
    }
}
