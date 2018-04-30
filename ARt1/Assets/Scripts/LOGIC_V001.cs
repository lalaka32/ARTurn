using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class LOGIC_V001 : MonoBehaviour {
    List<GameObject> masactivecars = new List<GameObject>();
    string question;
    Car[] cars;
    public GameObject[] MasCars { private get; set; }


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
    IEnumerator StartNextMoment()
    {
        //if (masactivecars == null)
        //{
        //    StopCoroutine("StartNextMoment");
        //}
        while (masactivecars.Count != 0)
        {
            for (int i = 0; i < masactivecars.Count; i++)
            {
                if (masactivecars[i].GetComponent<Car>().isstop == true)
                {
                    masactivecars.RemoveAt(i);
                    Debug.Log("remove" + i);
                }
            }
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("stop startnextmoment");
    }
    IEnumerator StartByPrioritets()
    {
        for (int j = 0; j < MasCars.Length; j++)//цикл приоритетов
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
            yield return new WaitWhile(()=> masactivecars.Count != 0);

        }



    }
    public void MakePriorities(Priority playerPriority)
    {
        cars = new Car[MasCars.Length];
        for (int i = 0; i < MasCars.Length; i++)
        {
            cars[i] = MasCars[i].GetComponent<Car>();
        }
        //masCars[0].GetComponent<Car>().priority = playerPriority;
        Directionitatible carDir;
        for (int i = 0; i < MasCars.Length; i++)
        {
            
            switch (MasCars[i].GetComponent<Car>().direction)
            {
                case Direction.forward:
                    carDir = new DirectionForward(cars, i);
                    carDir.SetPriority();
                    break;
                case Direction.right:
                    carDir = new DirectionRight(cars, i);
                    carDir.SetPriority();
                    break;
                case Direction.left:
                    carDir = new DirectionLeft(cars, i);
                    carDir.SetPriority();
                    break;
            }
           
        }
        MakeLogicOnAns();
    }

}
public abstract class Directionitatible
{
    public int index;
    public Directionitatible(Car[] car, int index)
    {
        Car = car;
        this.index = index;
    }

    public Car[] Car { get; set; }
    public abstract void SetPriority();

}

public class DirectionLeft : Directionitatible
{
    public DirectionLeft(Car[] car, int index) : base(car, index){}
    public bool continueturnleft = true;
    public override void SetPriority()
    {
        Car[index].Position--;
        for (int i = 0; i < Car.Length; i++)//к правой тачке
        {
            if (Car[i].Position == Car[index].Position && i != index && Car[i].direction == Direction.forward)
            {
                Car[index].priority++;
                Car[index].Position--;
                for (int j = 0; j < Car.Length; j++)//к встечной тачке когда есть правая
                {
                    if (Car[j].Position == Car[index].Position && j != index)
                    {
                        Car[index].priority++;
                        break;
                    }
                }
                Car[index].Position += 2;
                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
                return;
            }
            else if (Car[i].Position == Car[index].Position && i != index && Car[i].direction == Direction.left)
            {
                Car[index].priority++;
                Car[index].Position--;
                for (int j = 0; j < Car.Length; j++)//к встечной тачке когда есть правая
                {
                    if (Car[j].Position == Car[index].Position && j != index)
                    {
                        Car[index].priority++;
                        Car[index].Position += 2;
                        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
                        return;
                    }
                }
                //если встречной нет
                Car[index].Position--;
                for (int j = 0; j < Car.Length; j++)//к левой тачке когда есть правая
                {
                    if (Car[j].Position == Car[index].Position && j != index && Car[j].direction == Direction.right)//к левой тачке, которая направо, когда есть правая тачка налево
                    {
                        Car[index].priority++;
                        break;
                    }
                }
                Car[index].Position += 3;
                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
                return;
            }
            else if (Car[i].Position == Car[index].Position && i != index && Car[i].direction == Direction.right)
            {
                Car[index].priority++;
                Car[index].Position++;
                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
                return;
            }
        }
        Car[index].Position--;
        for (int i = 0; i < Car.Length; i++)//к встречной тачке
        {
            if (Car[i].Position == Car[index].Position && i != index && Car[i].direction == Direction.right)
            {
                Car[index].priority++;
                break;
            }
            else if (Car[i].Position == Car[index].Position && i != index && Car[i].direction != Direction.right)
            {
                Car[index].Position--;
                for (int j = 0; j < Car.Length; j++)
                {
                    if (Car[j].Position == (Car[index].Position) && j != index)
                    {
                        Car[index].Position += 3;
                        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
                        return;
                    }
                }
                Car[index].priority++;
                Car[index].Position++;
                break;
            }
        }
        Car[index].Position += 2;
        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
        return;

    }
}
public class DirectionRight : Directionitatible
{
    public DirectionRight(Car[] car, int index) : base(car, index){}
    public override void SetPriority()
    {
        Car[index].priority = Priority.first;
        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
    }
}
public class DirectionForward : Directionitatible
{
    public DirectionForward(Car[] car, int index) : base(car, index){}
    public override void SetPriority()
    {
        Car[index].Position--;
        for (int i = 0; i < Car.Length; i++)//к правой тачке
        {
            if (Car[i].Position == Car[index].Position && i != index && Car[i].direction == Direction.forward)
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
                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
                return;
            }
            else if (Car[i].Position == Car[index].Position && i != index && Car[i].direction == Direction.left)
            {
                Car[index].priority++;
                Car[index].Position--;
                for (int j = 0; j < Car.Length; j++)//к встречной тачке
                {
                    if (Car[j].Position == Car[index].Position && j != index)
                    {
                        Car[index].priority++;
                        Car[index].Position += 2;
                        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
                        return;
                    }
                }

                Car[index].Position--;
                for (int j = 0; j < Car.Length; j++)
                {
                    if (Car[j].Position == Car[index].Position && j != index && Car[j].direction == Direction.right)
                    {
                        Car[index].priority++;
                        break;
                    }
                }
                Car[index].Position += 3;
                Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
                return;
            }
            else if (Car[i].Position == Car[index].Position && i != index && Car[i].direction == Direction.right)
            {
                Car[index].priority++;
                break;
            }
        }
        Car[index].Position++;
        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
        return;
    }
}

