using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Car : MonoBehaviour {
    public Priority priority;
    public Direction direction;
    
    private void Awake()
    {
        direction=(Direction)Random.Range(0, 3);
        
    }
    public void StartAnime()
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
            
        }
    }
}
