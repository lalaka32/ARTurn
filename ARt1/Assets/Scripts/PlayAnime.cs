using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayAnime : MonoBehaviour
{
    public void MoveForward()
    {
        GetComponent<Animator>().SetBool("Isforward", true);
    }
    public void MoveLeft()
    {
        GetComponent<Animator>().SetBool("Isleft", true);
    }
    public void MoveRight()
    {
        GetComponent<Animator>().SetBool("Isright", true);
    }
    public void MoveReturn()
    {
        GetComponent<Animator>().SetBool("Isreturn", true);
    }
}
