using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Car : MonoBehaviour {
    public Priority priority;
    public Direction direction;
    private void Awake()
    {
        direction = Direction.right;
    }
    public void PlayAnime()
    {
        switch (GetComponent<Car>().direction)
        {
            case Direction.forward:
                GetComponent<Animator>().SetBool("Isforward", true);
                break;
            case Direction.left:
                GetComponent<Animator>().SetBool("Isleft", true);
                break;
            case Direction.right:
                GetComponent<Animator>().SetBool("Isright", true);
                break;
            case Direction.turn:
                GetComponent<Animator>().SetBool("Isreturn", true);
                break;
        }
    }
}
