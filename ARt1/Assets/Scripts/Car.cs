using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Car : MonoBehaviour
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
    private void Awake()
    {
        direction = (Direction)Random.Range(0, 3);

    }
    public void StartAnime()
    {
        switch (GetComponent<Car>().direction)
        {
            case Direction.forward:
                GetComponent<Animator>().Play("CarForwardanim");
                break;
            case Direction.left:
                GetComponent<Animator>().Play("CarLeftanim");
                break;
            case Direction.right:
                GetComponent<Animator>().Play("CarRightanim");
                break;
        }
    }
    public void Isstop()
    {
        isstop = true;
        Debug.Log("isstop = true;");
    }
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Debug.Log("Collision!!!");

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>() == collision.collider)
        {
            Debug.Log("Collision!!!");
            //Destroy(gameObject.GetComponent<Animator>());
            //Destroy(collision.gameObject.GetComponent<Animator>());
        }



        Debug.Log("bye");
    }
}
