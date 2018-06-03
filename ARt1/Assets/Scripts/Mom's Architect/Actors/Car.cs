using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BeeFly
{
    class Car:MonoBehaviour
    {
        public Priority priority;
        public Direction direction;
        private Position _position;
        public bool isstop = false;
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

        public Direction Direction { get; internal set; }

        private void Awake()
        {
            direction = (Direction)UnityEngine.Random.Range(2, 3);

        }
        public void StartAnime()
        {
            switch (GetComponent<Car>().direction)
            {
                case Direction.Forward:
                    GetComponent<Animator>().Play("CarForwardanim");
                    break;
                case Direction.Left:
                    GetComponent<Animator>().Play("CarLeftanim");
                    break;
                case Direction.Right:
                    GetComponent<Animator>().Play("CarRightanim");
                    break;
            }
        }
        public void Isstop()
        {
            isstop = true;
            Debug.Log("isstop = true;");
        }
    }
}
