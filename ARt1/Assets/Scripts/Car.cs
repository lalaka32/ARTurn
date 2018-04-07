using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Car : MonoBehaviour
{
    public Priority priority;
    public Direction direction;
    private Position _position;

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
    private void Awake()
    {
        direction = (Direction)Random.Range(0, 2);

    }
    public void StartAnime()
    {
        //switch (GetComponent<Car>().direction)
        //{
        //    case Direction.forward:
        //        GetComponent<Animator>().SetBool("Isforward", true);
        //        break;
        //    case Direction.left:
        //        GetComponent<Animator>().SetBool("Isleft", true);
        //        break;
        //    case Direction.right:
        //        GetComponent<Animator>().SetBool("Isright", true);
        //        break;

        //}
    }
}
