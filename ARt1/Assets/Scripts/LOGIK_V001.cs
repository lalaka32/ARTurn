using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class LOGIK_V001 : MonoBehaviour {

    string question;
    Dictionary<GameObject, byte> bigDick;
    GameObject[] masCars;
    // Use this for initialization
    void Start () {
        masCars = GetComponent<Instantiate>().masCars;
        // METOD V INSTANCIATE
        
    }
    void InitAnsUser(byte playerPriority)
    {
        bigDick.Add(masCars[0], playerPriority);

    }
    //создание на основе вопроса очерёдности
    public void MakeLogicOnAns()
    {

        switch (GetComponent<Instantiate>().trafficLight)
        {
            case TrafficLight.off://по умолчанию
                masCars[0].GetComponent<Car>().PlayAnime();
                masCars[1].GetComponent<Car>().Invoke("PlayAnime", 3f);

                break;
            case TrafficLight.green:
                break;
            case TrafficLight.red:
                break;
        }
    }
    public void MakePriorities(Priority playerPriority)
    {
        masCars[0].GetComponent<Car>().priority = playerPriority;
        masCars[1].GetComponent<Car>().priority = Priority.second;
        MakeLogicOnAns();
    }
	// Update is called once per frame
	void Update () {
		
	}

}
