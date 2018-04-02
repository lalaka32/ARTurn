using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Car : MonoBehaviour {
    public Priority priority;
    public Direction direction;
    public float delay;
    private void Awake()
    {
        direction = Direction.turn; /*(Direction)Random.Range(0, 3);*/
        delay = 0f;
    }
    public void SetAnime()
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
    public void StartAnime()
    {
        if (delay == 0f)
        {
            SetAnime();
            Debug.Log("JEPPA");
        }
        else
        {
            Debug.Log("HUIII");
            Invoke("SetAnime", delay);
        }
    }
}
