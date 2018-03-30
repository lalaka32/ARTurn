using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayAnime : MonoBehaviour
{
    public GameObject[] masCars;
    public Animator[] animators;
    //public Object[] controllers = new Object[4];
    private void Start()
    {
        animators =  new Animator[4];
    }
    public void MoveForward()
    {
        masCars = GetComponent<Instantiate>().masCars;
        masCars[0].GetComponent<Animator>().SetBool("Isforward", true);
        masCars[1].GetComponent<Animator>().SetBool("Isreturn", true);
        masCars[2].GetComponent<Animator>().SetBool("Isforward", true);
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
