using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

class ForLogicData
{
    public GameObject MasCars { get; set; }

}
public class LOGIC_V001 : MonoBehaviour {

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
        for (int j = 0; j < masCars.Length; j++)
        {
            
            for (int i = 0; i < masCars.Length; i++)
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
        for (int i = 1; i <= masCars.Length; i++)
        {
            switch (masCars[i].GetComponent<Car>().direction)
            {
                case Direction.forward:
                    for (int j = 0; j < masCars.Length; j++)
                    {
                        if (masCars[j].name == "p"+(i-1))
                        {
                            masCars[i].GetComponent<Car>().priority++; 
                        }
                    }
                    break;

                case Direction.left:
                    for (int j = 0; j < masCars.Length; j++)
                    {
                        if (masCars[j].name == "p" + (i - 1)||masCars[j].name =="p"+(i-2))
                        {
                            masCars[i].GetComponent<Car>().priority++;
                        }
                    }
                    break;

                case Direction.right:
                    masCars[1].GetComponent<Car>().priority = Priority.first;
                    break;
            }
        }
        

        MakeLogicOnAns();
        
        
        
    }
	// Update is called once per frame
	void Update () {
		
	}

}
