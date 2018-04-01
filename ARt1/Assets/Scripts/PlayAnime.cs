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
        GetComponent<LOGIK_V001>().MakeLogicOnAns(Priority.first);
    }

    public void MoveSecond()
    {
        //InitAnsUser( 2);
    }
    public void MoveThird()
    {
        //InitAnsUser( 3);
    }
    public void MoveForth()
    {
        //InitAnsUser( 4a);
    }
}
