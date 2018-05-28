using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Threading;

public class LOGIC_V001 : MonoBehaviour {
    List<GameObject> masactivecars = new List<GameObject>();
    List<GameObject> masVIPs = new List<GameObject>();
    List<GameObject> masGreenCars = new List<GameObject>();
    List<GameObject> masRedCars = new List<GameObject>();
    public Dictionary<Position, Car> listOfpositions = new Dictionary<Position, Car>();
    int nextprior = 0;
    //string question;
    public GameObject[] MasCars {  get; set; }
    Priority playerOne;

    public void MakeLogicOnAns(int player)//Главный
    {
        Clear();
        MasCars = ToolBox.Get<CarManager>().MasCars;
        playerOne = (Priority)player;
        MakePrioritiesOff();
        SetGreenRedAndVIPsCars();
        StartCars();
    }

    public void StartCars()
    {
        if (ToolBox.Get<TrafficLightManager>().PosTL == null)
        {
            StartCoroutine(StartByPrioritets(MasCars, 0));
        }
        else
        {
            StartCoroutine(GroupCarsStart(masVIPs, false));
            StartCoroutine(GroupCarsStart(masGreenCars, masVIPs, false));
            StartCoroutine(GroupCarsStart(masRedCars, masGreenCars, true));
        }

    }
    //Пригодится не только для светофоров
    IEnumerator GroupCarsStart(List<GameObject> cars, List<GameObject> carsBefore, bool isReverseTrafficLights)
    {
        yield return new WaitUntil(() => carsBefore.Count == 0);
        if (isReverseTrafficLights)
        {
            StartCoroutine(ReverseLights());
            yield return new WaitForSeconds(1.5f);
        }
        Debug.Log("GOGOGOO");
        foreach (var item in cars)
        {
            Debug.Log(item.name);
        }
        StartCoroutine(StartByPrioritets(cars.ToArray(), nextprior));

    }
    IEnumerator GroupCarsStart(List<GameObject> cars, bool isReverseTrafficLights)
    {
        if (isReverseTrafficLights)
        {
            StartCoroutine(ReverseLights());
            yield return new WaitForSeconds(2);
        }
        Debug.Log("GOGOGOO");
        foreach (var item in cars)
        {
            Debug.Log(item.name);
        }
        StartCoroutine(StartByPrioritets(cars.ToArray(), nextprior));

    }
    IEnumerator ReverseLights()
    {
        for (int i = 0; i < ToolBox.Get<TrafficLightManager>().TL.Length; i++)
        {
            //GameObject cashlight = TL[i].transform.Find("turner(Clone)");
            //if (ToolBox.Get<TrafficLightManager>().TL[i].transform.Find("turner(Clone)") != null)
            //{
            Transform tl1 = ToolBox.Get<TrafficLightManager>().TL[i].transform.Find("turner(Clone)");
            tl1.name = "turner(Clone)-preinversed";
            Transform tl2 = ToolBox.Get<TrafficLightManager>().TL[i].transform.Find("turner(Clone)");
            tl2.name = "turner(Clone)-preinversed";
            tl1.localPosition = new Vector3(0, 20, -4);
            tl2.localPosition = new Vector3(0, 20, 4);
            tl1.GetComponent<Light>().color = Color.yellow;
            tl2.GetComponent<Light>().color = Color.yellow;
        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < ToolBox.Get<TrafficLightManager>().TL.Length; i++)
        {
            Transform tl1 = ToolBox.Get<TrafficLightManager>().TL[i].transform.Find("turner(Clone)-preinversed");
            tl1.name = "turner(Clone)-inversed";
            Transform tl2 = ToolBox.Get<TrafficLightManager>().TL[i].transform.Find("turner(Clone)-preinversed");
            tl2.name = "turner(Clone)-inversed";
            if (ToolBox.Get<TrafficLightManager>().PosTL[(Position)i] == TrafficLight.green)
            {
                tl1.GetComponent<Light>().color = Color.red;
                tl2.GetComponent<Light>().color = Color.red;
                tl1.localPosition = new Vector3(0, 22, -4);
                tl2.localPosition = new Vector3(0, 22, 4);
            }
            else
            {
                tl1.GetComponent<Light>().color = Color.green;
                tl2.GetComponent<Light>().color = Color.green;
                tl1.localPosition = new Vector3(0, 18, -4);
                tl2.localPosition = new Vector3(0, 18, 4);
            }
        }
    }


    public void SetGreenRedAndVIPsCars()
    {
        if (ToolBox.Get<TrafficLightManager>().PosTL != null)
        {
            for (int i = 0; i < 4; i++)
            {
                if (listOfpositions.ContainsKey((Position)i))
                {
                    if (listOfpositions[(Position)i].tag != "VIP")
                    {
                        if (ToolBox.Get<TrafficLightManager>().PosTL[(Position)i] == TrafficLight.green)
                        {
                            masGreenCars.Add(listOfpositions[(Position)i].gameObject);
                        }
                        else masRedCars.Add(listOfpositions[(Position)i].gameObject);
                    }
                    else masVIPs.Add(listOfpositions[(Position)i].gameObject);
                }
            }
        }

    }

    public void MakePrioritiesOff()
    {
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
        int maxprior = 0;
        foreach (var item in cars)
        {
            if ((int)item.GetComponent<Car>().priority > maxprior)
            {
                maxprior = (int)item.GetComponent<Car>().priority;
            }
        }

            Debug.Log("StartByPrioritets");
        for (int j = startprior; j <= maxprior; j++)//цикл приоритетов
        {
            Debug.Log(startprior);
            //masactivecars.Clear();
            for (int i = 0; i < cars.Length; i++)
            {
                if (cars[i].GetComponent<Car>().priority == (Priority)j)
                {
                    masactivecars.Add(cars[i]);
                    cars[i].GetComponent<Car>().StartAnime();
                }
            }
            StartCoroutine(StartNextMoment());
            startprior++;
            yield return new WaitWhile(() => masactivecars.Count != 0);

        }
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
                    masVIPs.Remove(masactivecars[i]);
                    masactivecars.Remove(masactivecars[i]);
                }
            }
            //Debug.Log("greencars"+masGreenCars.Count);
            yield return new WaitForEndOfFrame();
        }
    }

    public void Clear()
    {
        StopAllCoroutines();
        listOfpositions.Clear();
        masRedCars.Clear();
        masGreenCars.Clear();
        masVIPs.Clear();
        masactivecars.Clear();
        nextprior = 0;
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
