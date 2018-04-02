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
        GetComponent<LOGIK_V001>().MakePriorities(Priority.first);
    }

    public void MoveSecond()
    {
        GetComponent<LOGIK_V001>().MakePriorities(Priority.second);

    }
    public void MoveThird()
    {
        GetComponent<LOGIK_V001>().MakePriorities(Priority.third);
    }
    public void MoveForth()
    {
        GetComponent<LOGIK_V001>().MakePriorities(Priority.fourth);
    }
}
