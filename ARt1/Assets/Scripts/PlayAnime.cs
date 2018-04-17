using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Enums;
public class PlayAnime : MonoBehaviour
{

    public GameObject[] masCars;
    //public Object[] controllers = new Object[4];
    public void MoveFirst()
    {

        GetComponent<LOGIC_V001>().MakePriorities(Priority.first);
    }

    public void MoveSecond()
    {
        GetComponent<LOGIC_V001>().MakePriorities(Priority.second);

    }
    public void MoveThird()
    {
        GetComponent<LOGIC_V001>().MakePriorities(Priority.third);
    }
    public void MoveForth()
    {
        GetComponent<LOGIC_V001>().MakePriorities(Priority.fourth);
    }
}
