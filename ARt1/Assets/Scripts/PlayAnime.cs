using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayAnime : MonoBehaviour
{
    public GameObject[] masCars;
    public Animator[] animators = new Animator[4];
    //public Object[] controllers = new Object[4];
    private void Start()
    {
        MoveForward();
    }
    public void MoveForward()
    {
        masCars = GetComponent<Instantiate>().masCars;
        animators[0] = masCars[0].GetComponent<Animator>();
        animators[0].SetBool("IsIsforward", true);
        //animators[1].SetBool("Isreturn", true);
    }
    public void MoveLeft()
    {
        masCars = GetComponent<Instantiate>().masCars;
        masCars[0].GetComponent<Animator>().SetBool("Isleft", true);
    }
    public void MoveRight()
    {
        masCars = GetComponent<Instantiate>().masCars;
        masCars[0].GetComponent<Animator>().SetBool("Isright", true);
    }
    public void MoveReturn()
    {
        masCars = GetComponent<Instantiate>().masCars;
        masCars[0].GetComponent<Animator>().SetBool("Isreturn", true);
    }
}
