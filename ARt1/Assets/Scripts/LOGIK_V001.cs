using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class LOGIK_V001 : MonoBehaviour {

    string question;
    GameObject[] masCars;
    GameObject[] prioritymas;
    // Use this for initialization
    void Start () {
        masCars = GetComponent<Instantiate>().masCars;
        prioritymas = new GameObject[2];
        // METOD V INSTANCIATE

    }   
    //создание очерёдности
    public void MakeLogicOnAns()
    {

        switch (GetComponent<Instantiate>().trafficLight)
        {
            case TrafficLight.off://по умолчанию
                StartCoroutine(StartByPrioritets());
                break;
            case TrafficLight.green:
                break;
            case TrafficLight.red:
                break;
        }
    }
    IEnumerator StartByPrioritets()
    { 
        for (int j = 0; j < 2; j++)
        {
            
            for (int i = 0; i < 2; i++)
            {
                if (masCars[i].GetComponent<Car>().priority == (Priority)j)
                {
                    masCars[i].GetComponent<Car>().StartAnime();
                }
            }
            yield return new WaitForSeconds(1);

        }


    }
    public void MakePriorities(Priority playerPriority)
    {
        masCars[0].GetComponent<Car>().priority = playerPriority;
        switch (masCars[1].GetComponent<Car>().direction)
        {
            case Direction.forward://!!!
            //добавить if на разворот 1го авто с делеем и приоритетом first как в 
            //case Direction.turn:
                masCars[1].GetComponent<Car>().priority = Priority.second;
                
                break;

            case Direction.left:
                //Пока затруднился придумать условие, т.к. не понял как поступать машине 2 при
                //повороте влево, если машина 1 делает разворот(правило правой руки в этом случае
                //применяется по разу на каждую машину, этот конфликт из-за того, что дороги 
                //двухполосные.
                //в остальных ситуациях вроде легко
                break;

            case Direction.right:
                masCars[1].GetComponent<Car>().priority = Priority.first;
                break;

            case Direction.turn:
                if (masCars[0].GetComponent<Car>().direction == Direction.turn)
                {//решил проблему разворота 2й машины при развороте 1й довольно неплохо, 
                    //думаю так и оставить(с помощью задержки анимации 2й, но при этом не меняя
                    //её приоритет, что считаю правильным)
                    masCars[1].GetComponent<Car>().delay = 1f;
                    masCars[1].GetComponent<Car>().priority = Priority.first;
                }
                else
                    masCars[1].GetComponent<Car>().priority = Priority.second;
                break;

        }
        MakeLogicOnAns();
        
        
        
    }
	// Update is called once per frame
	void Update () {
		
	}

}
