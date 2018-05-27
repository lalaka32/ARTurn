using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Threading;

public class LOGIC_V001 : MonoBehaviour {
    List<GameObject> masactivecars = new List<GameObject>();
    List<GameObject> masGreenCars = new List<GameObject>();
    List<GameObject> masRedCars = new List<GameObject>();
    int countGreenCars, countRedCars, nextprior;
    bool corend = false;
    //string question;
    public GameObject[] MasCars {  get; set; }
    Priority playerOne;
    public Dictionary<Position, Car> listOfpositions;
    public void MakeLogicOnAns(int player)
    {
        nextprior = 0;
        MasCars = ToolBox.Get<CarManager>().MasCars;
        playerOne = (Priority)player;
        MakePrioritiesOff();
        SetGreenAndRedCars();
        listOfpositions.Clear();

        StartCars();
        //Debug.Log("Clear");
        //masGreenCars.Clear();
        //masRedCars.Clear();
    }

    public void StartCars()
    {
        if (ToolBox.Get<TrafficLightManager>().PosTL == null)
        {
            StartCoroutine(StartByPrioritets(MasCars, 0));
        }
        else
        {
            StartCoroutine(StartByPrioritets(masGreenCars.ToArray(), 0));

            StartCoroutine(RedStart());
        }

    }
    IEnumerator RedStart()
    {
        yield return new WaitUntil(() => masGreenCars.Count == 0);
        Debug.Log("GOGOGOO");
        StartCoroutine(StartByPrioritets(masRedCars.ToArray(), nextprior));
    }


    public void SetGreenAndRedCars()
    {
        countRedCars = 0;
        countGreenCars = 0;
        masRedCars.Clear();
        masGreenCars.Clear();
        if (ToolBox.Get<TrafficLightManager>().PosTL != null)
        {
            for (int i = 0; i < 4; i++)
            {
                if (listOfpositions.ContainsKey((Position)i))
                {
                    if (ToolBox.Get<TrafficLightManager>().PosTL[(Position)i] == TrafficLight.green)
                    {
                        masGreenCars.Add(listOfpositions[(Position)i].gameObject);
                    }
                    else masRedCars.Add(listOfpositions[(Position)i].gameObject);
                }
            }
        }
        countGreenCars = masGreenCars.Count;
        countRedCars = masRedCars.Count;
        foreach (var item in masRedCars)
        {
            Debug.Log(item.name);
        }

    }

    public void MakePrioritiesOff()
    {
        listOfpositions = new Dictionary<Position, Car>();
        for (int i = 0; i < MasCars.Length; i++)
        {
            listOfpositions.Add(MasCars[i].GetComponent<Car>().Position, MasCars[i].GetComponent<Car>());
        }
        for (int i = 0; i < MasCars.Length; i++)
        {
            Car settingCar = MasCars[i].GetComponent<Car>(); ;
            Dictionary<ComperativeLocation, Car> comperative = GetComperative(listOfpositions, settingCar);
            MasCars[i].GetComponent<Car>().SetPriority(comperative, settingCar);
        }
    }
    Dictionary<ComperativeLocation, Car> GetComperative(Dictionary<Position, Car> listOfPositions, Car settingCar)
    {
        
        Car observeCar;
        Dictionary<ComperativeLocation, Car> dicWithСomparative = new Dictionary<ComperativeLocation, Car>();

        for (int i = 0; i < 3; i++)
        {
            settingCar.Position--;
            if (listOfPositions.TryGetValue(settingCar.Position, out observeCar)&&observeCar.tag!="VIP")
            {
                dicWithСomparative.Add((ComperativeLocation)i, observeCar);
            }
        }
        settingCar.Position--;

        return dicWithСomparative;
    }


    IEnumerator StartByPrioritets(GameObject[] cars, int startprior)
    {
        int CountOfPrioritets = cars.Length;
            Debug.Log("StartByPrioritets");
        for (int j = startprior; j < CountOfPrioritets + startprior; j++)//цикл приоритетов
        {
            Debug.Log(nextprior);
            masactivecars.Clear();
            for (int i = 0; i < cars.Length; i++)
            {
                if (cars[i].GetComponent<Car>().priority == (Priority)j)
                {
                    masactivecars.Add(cars[i]);
                    cars[i].GetComponent<Car>().StartAnime();
                }
            }
            StartCoroutine(StartNextMoment());
            nextprior++;
            yield return new WaitWhile(() => masactivecars.Count != 0);

        }
        corend = true;
    }

    IEnumerator StartNextMoment()
    {
        while (masactivecars.Count != 0)
        {
            for (int i = 0; i < masactivecars.Count; i++)
            {
                if (masactivecars[i].GetComponent<Car>().isstop == true)
                {
                    masGreenCars.Remove(masactivecars[i]);
                    masRedCars.Remove(masactivecars[i]);
                    masactivecars.RemoveAt(i);
                }
            }
            Debug.Log("greencars"+masGreenCars.Count);
            yield return new WaitForEndOfFrame();
        }
    }



    int processingAnswer()
    {
        int length;
        if ((int)MasCars[0].GetComponent<Car>().priority == (int)playerOne)
        {
            length = MasCars.Length;
        }
        else if ((int)MasCars[0].GetComponent<Car>().priority < (int)playerOne)
        {
            length = ((int)MasCars[0].GetComponent<Car>().priority);
        }
        else
        {

            if ((int)MasCars[0].GetComponent<Car>().priority - (int)playerOne == 2)
            {
                MasCars[0].GetComponent<Car>().priority = playerOne;
                length = 1;
            }
            else
            {
                length = ((int)MasCars[0].GetComponent<Car>().priority);
                MasCars[0].GetComponent<Car>().priority = playerOne;
            }
        }

        return length;
    }

}
