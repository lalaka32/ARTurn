using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class LOGIC_V001 : MonoBehaviour {
    List<GameObject> masactivecars = new List<GameObject>();
    //string question;
    public GameObject[] MasCars { private get; set; }
    Priority playerOne;
    public Dictionary<Position, Car> listOfpositions;
    public void MakeLogicOnAns(int player)
    {
        playerOne = (Priority)player;
        MakePrioritiesOff();
        listOfpositions.Clear();
        StartCoroutine(StartByPrioritets());
        switch (ToolBox.Get<CrossManager>().trafficLight)
        {
            case TrafficLight.off://по умолчанию
                break;
            case TrafficLight.green:
                break;
            case TrafficLight.red:
                break;
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
            if (listOfPositions.TryGetValue(settingCar.Position, out observeCar))
            {
                dicWithСomparative.Add((ComperativeLocation)i, observeCar);
            }
        }
        settingCar.Position--;

        return dicWithСomparative;
    }
    IEnumerator StartByPrioritets()
    {
        int CountOfPrioritets = MasCars.Length;

        for (int j = 0; j < CountOfPrioritets; j++)//цикл приоритетов
        {
            masactivecars.Clear();
            for (int i = 0; i < MasCars.Length; i++)
            {
                if (MasCars[i].GetComponent<Car>().priority == (Priority)j)
                {
                    masactivecars.Add(MasCars[i]);
                    MasCars[i].GetComponent<Car>().StartAnime();
                }
            }
            StartCoroutine(StartNextMoment());
            yield return new WaitWhile(() => masactivecars.Count != 0);

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

    IEnumerator StartNextMoment()
    {
        while (masactivecars.Count != 0)
        {
            for (int i = 0; i < masactivecars.Count; i++)
            {
                if (masactivecars[i].GetComponent<Car>().isstop == true)
                {
                    masactivecars.RemoveAt(i);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
public interface IDirectionitatible
{
     void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar);
}
public class DirectionRight : IDirectionitatible
{
    public void SetPriority(Dictionary<ComperativeLocation, Car> comperative, Car settingCar)
    {
        settingCar.priority = Priority.first;

        Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
    }
}