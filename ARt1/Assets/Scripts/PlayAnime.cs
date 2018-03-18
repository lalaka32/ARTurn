using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayAnime : MonoBehaviour
{
    public GameObject car;
    public void MoveForward()
    {
        car.GetComponent<Animator>().SetBool("Isforward", true);
    }
    public void MoveLeft()
    {
        car.GetComponent<Animator>().SetBool("Isleft", true);
    }
    public void MoveRight()
    {
        car.GetComponent<Animator>().SetBool("Isright", true);
    }
    public void MoveReturn()
    {
        car.GetComponent<Animator>().SetBool("Isreturn", true);
    }
}
