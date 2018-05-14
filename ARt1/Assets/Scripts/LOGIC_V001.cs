using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class LOGIC_V001 : MonoBehaviour {
    List<GameObject> masactivecars = new List<GameObject>();
    //string question;
    Car[] cars;
    public GameObject[] MasCars { private get; set; }
    Priority playerOne;
    public Dictionary<Position, Car> listOfpositions;
    public void MakeLogicOnAns(int player)
    {
        playerOne = (Priority)player;
        switch (GetComponent<Instantiate>().trafficLight)
        {
            case TrafficLight.off://по умолчанию
                MakePrioritiesOff();
                listOfpositions.Clear();
                StartCoroutine(StartByPrioritets());
                break;
            case TrafficLight.green:
                break;
            case TrafficLight.red:
                break;
        }
    }
    public void MakePrioritiesOff()
    {
        cars = new Car[MasCars.Length];
        listOfpositions = new Dictionary<Position, Car>();
        for (int i = 0; i < MasCars.Length; i++)
        {
            cars[i] = MasCars[i].GetComponent<Car>();
            listOfpositions.Add(MasCars[i].GetComponent<Car>().Position, cars[i]);
        }
        for (int i = 0; i < cars.Length; i++)
        {
            MasCars[i].GetComponent<Car>().SetPriority(cars, i, listOfpositions, MasCars[i].GetComponent<Car>().Position);

        }
    }
    IEnumerator StartByPrioritets()
    {
        int length = MasCars.Length;
        //if ((int)MasCars[0].GetComponent<Car>().priority == (int)playerOne)
        //{
        //    length = MasCars.Length;
        //}
        //else if ((int)MasCars[0].GetComponent<Car>().priority < (int)playerOne)
        //{
        //    length = ((int)MasCars[0].GetComponent<Car>().priority);
        //}
        //else
        //{

        //    if ((int)MasCars[0].GetComponent<Car>().priority - (int)playerOne == 2)
        //    {
        //        MasCars[0].GetComponent<Car>().priority = playerOne;
        //        length = 1;
        //    }
        //    else
        //    {
        //        length = ((int)MasCars[0].GetComponent<Car>().priority);
        //        MasCars[0].GetComponent<Car>().priority = playerOne;
        //    }
        //}

        for (int j = 0; j < length; j++)//цикл приоритетов
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
     void SetPriority(Dictionary<Position, Car> listOfPositions,Position positionSetting);
}
public class DirectionRight : IDirectionitatible
{
    public void SetPriority( Dictionary<Position, Car> listOfPositions, Position positionSetting)
    {
        Car settingCar = listOfPositions[positionSetting];

        settingCar.priority = Priority.first;

        Debug.Log(" Pos : " + settingCar.Position + " direction : " + settingCar.Direction + " Prior : " + settingCar.priority);
    }
}
//public class DirectionLeft : IDirectionitatible
//{
//    public  void SetPriority(Car[] Car, int index,Dictionary<Position,Car> listOfPositions, Position positionSetting)
//    {
//        Car[index].Position--;
//        for (int i = 0; i < Car.Length; i++)//к правой тачке
//        {
//            if (Car[i].Position == Car[index].Position && i != index && Car[i].Direction == Direction.forward)
//            {
//                Car[index].priority++;
//                Car[index].Position--;
//                for (int j = 0; j < Car.Length; j++)//к встечной тачке когда есть правая
//                {
//                    if (Car[j].Position == Car[index].Position && j != index)
//                    {
//                        Car[index].priority++;
//                        break;
//                    }
//                }
//                Car[index].Position += 2;
//                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
//                return;
//            }
//            else if (Car[i].Position == Car[index].Position && i != index && Car[i].Direction == Direction.left)
//            {
//                Car[index].priority++;
//                Car[index].Position--;
//                for (int j = 0; j < Car.Length; j++)//к встечной тачке когда есть правая
//                {
//                    if (Car[j].Position == Car[index].Position && j != index)
//                    {
//                        Car[index].priority++;
//                        Car[index].Position += 2;
//                        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
//                        return;
//                    }
//                }
//                //если встречной нет
//                Car[index].Position--;
//                for (int j = 0; j < Car.Length; j++)//к левой тачке когда есть правая
//                {
//                    if (Car[j].Position == Car[index].Position && j != index && Car[j].Direction == Direction.right)//к левой тачке, которая направо, когда есть правая тачка налево
//                    {
//                        Car[index].priority++;
//                        break;
//                    }
//                }
//                Car[index].Position += 3;
//                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
//                return;
//            }
//            else if (Car[i].Position == Car[index].Position && i != index && Car[i].Direction == Direction.right)
//            {
//                Car[index].priority++;
//                Car[index].Position++;
//                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
//                return;
//            }
//        }
//        Car[index].Position--;
//        for (int i = 0; i < Car.Length; i++)//к встречной тачке
//        {
//            if (Car[i].Position == Car[index].Position && i != index && Car[i].Direction == Direction.right)
//            {
//                Car[index].priority++;
//                break;
//            }
//            else if (Car[i].Position == Car[index].Position && i != index && Car[i].Direction == Direction.forward)
//            {
//                Car[index].Position--;
//                for (int j = 0; j < Car.Length; j++)
//                {
//                    if (Car[j].Position == Car[index].Position && j != index)
//                    {
//                        Car[index].Position += 3;
//                        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
//                        return;
//                    }
//                    //else if (Car[j].Position == Car[index].Position && j != index && Car[j].direction == Direction.right)
//                    //{
//                    //    Car[index].priority += 2;
//                    //    Car[index].Position += 3;
//                    //    Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
//                    //    return;
//                    //}
//                }
//                Car[index].priority++;
//                Car[index].Position++;
//                break;
//            }
//            else if (Car[i].Position == Car[index].Position && i != index && Car[i].Direction == Direction.left)
//            {
//                Car[index].Position--;
//                for (int j = 0; j < Car.Length; j++)
//                {
//                    if (Car[j].Position == Car[index].Position && j != index)
//                    {
//                        Car[index].Position += 3;
//                        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
//                        return;
//                    }
//                }
//                Car[index].Position++;
//                break;
//            }
//        }
//        Car[index].Position += 2;
//        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
//        return;

//    }
//}
public class DirectionForward 
{
    public void SetPriority(Car[] Car, int index, Dictionary<Position, Car> listOfPositions, Position positionSetting)
    {
        Car[index].Position--;
        for (int i = 0; i < Car.Length; i++)//к правой тачке
        {
            if (Car[i].Position == Car[index].Position && i != index && Car[i].Direction == Direction.forward)
            {
                Car[index].priority++;
                Car[index].Position--;
                for (int j = 0; j < Car.Length; j++)
                {
                    if (Car[j].Position == Car[index].Position && j != index)
                    {
                        Car[index].priority++;
                        break;
                    }
                }
                Car[index].Position += 2;
                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
                return;
            }
            //
            else if (Car[i].Position == Car[index].Position && i != index && Car[i].Direction == Direction.left)
            {
                Car[index].priority++;
                Car[index].Position--;
                for (int j = 0; j < Car.Length; j++)//к встречной тачке
                {
                    if (Car[j].Position == Car[index].Position && j != index)
                    {
                        Car[index].priority++;
                        Car[index].Position += 2;
                        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
                        return;
                    }
                }

                Car[index].Position--;
                for (int j = 0; j < Car.Length; j++)
                {
                    if (Car[j].Position == Car[index].Position && j != index && Car[j].Direction == Direction.right)
                    {
                        Car[index].priority++;
                        break;
                    }
                }
                Car[index].Position += 3;
                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
                return;
            }
            else if (Car[i].Position == Car[index].Position && i != index && Car[i].Direction == Direction.right)
            {
                Car[index].priority++;
                break;
            }
        }
        Car[index].Position++;
        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].Direction + " Prior : " + Car[index].priority);
        return;
    }
}

