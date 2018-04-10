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
    Car[] cars;
    GameObject[] masCars;
    // Use this for initialization
    void Start () {

        // METOD V INSTANCIATE
        masCars = GetComponent<Instantiate>().MasCars;
    }
    private void Update()
    {
        masCars = GetComponent<Instantiate>().MasCars;
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
        cars = new Car[masCars.Length];
        for (int i = 0; i < masCars.Length; i++)
        {
            cars[i] = masCars[i].GetComponent<Car>();
        }
        //masCars[0].GetComponent<Car>().priority = playerPriority;
        Prioritatible carDir;
        for (int i = 0; i < masCars.Length; i++)
        {
            
            switch (masCars[i].GetComponent<Car>().direction)
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
public abstract class Prioritatible
{
    public int index;
    public Prioritatible(Car[] car, int index)
    {
        Car = car;
        this.index = index;
    }

    public Car[] Car { get; set; }
    public abstract void SetPriority();

}

public class DirectionLeft : Prioritatible
{
    public DirectionLeft(Car[] car, int index) : base(car, index){}
    public bool continueturnleft = true;//есть помыслы добавить анимации левого поворота событие для изменения данной переменной
    public override void SetPriority()
    {
        for (int i = 0; i < Car.Length; i++)
        {
            if (Car[i].Position == (Car[index].Position - 1) && i != index && Car[i].direction != Direction.right)
            {
                Car[index].priority = Car[i].priority + 1;
            }
        }
    }
}
public class DirectionRight : Prioritatible
{
    public DirectionRight(Car[] car, int index) : base(car, index){}

    public override void SetPriority()
    {
        Car[index].priority = Priority.first;
        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " + Car[index].priority);
    }
}
public class DirectionForward : Prioritatible
{
    public DirectionForward(Car[] car, int index) : base(car, index){}
    public override void SetPriority()
    {
        for (int i = 0; i < Car.Length; i++)
        {
            if (Car[i].Position == (Car[index].Position-1) && i != index)
            {
                Car[index].priority = Car[i].priority + 1;
            }
        }
        Debug.Log(" Pos : " + Car[index].Position + " direction : " + Car[index].direction + " Prior : " +Car[index].priority );
    }
}

