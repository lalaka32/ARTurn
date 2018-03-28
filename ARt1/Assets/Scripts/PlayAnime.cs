using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayAnime : MonoBehaviour
{
    public Animator[] animators = new Animator[4];
    public Object[] ani = new Object[4];
    private void Start()
    {
        
    }
    public void MoveForward()
    {
        animators[0].SetBool("Isforward", true);
        animators[1].SetBool("Isreturn", true);
    }
    public void MoveLeft()
    {
        animators[0].SetBool("Isleft", true);
    }
    public void MoveRight()
    {
        animators[0].SetBool("Isright", true);
    }
    public void MoveReturn()
    {
        animators[0].SetBool("Isreturn", true);
    }
}
