using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class LOGIK_V001 : MonoBehaviour {

    string question;
    Car[] cars;
    GameObject[] masCars;
    // Use this for initialization
    void Start () {

        // METOD V INSTANCIATE
        masCars = GetComponent<Instantiate>().masCars;
    }
    private void Update()
    {
        masCars = GetComponent<Instantiate>().masCars;
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
        masCars[0].GetComponent<Car>().priority = playerPriority;
        for (int i = 0; i < masCars.Length; i++)
        {
            switch(masCars[i].GetComponent<Car>().direction)
            {
                case Direction.forward:
                    Prioritatible carforw = new DirectionForward(cars, i);
                    carforw.SetPriority();
                    break;
            }
            //switch (masCars[i].GetComponent<Car>().direction)
            //{
            //    case Direction.forward:
            //        for (int j = 0; j < masCars.Length; j++)
            //        {
            //            if (masCars[j].name == "p"+(i-1))
            //            {
            //                masCars[i].GetComponent<Car>().priority++; 
            //            }
            //        }
            //        break;

            //    case Direction.left:
            //        //Пока затруднился придумать условие, т.к. не понял как поступать машине 2 при
            //        //повороте влево, если машина 1 делает разворот(правило правой руки в этом случае
            //        //применяется по разу на каждую машину, этот конфликт из-за того, что дороги 
            //        //двухполосные.
            //        //в остальных ситуациях вроде легко
            //        break;

            //    case Direction.right:
            //        masCars[1].GetComponent<Car>().priority = Priority.first;
            //        break;
            //}
        }
        

        //MakeLogicOnAns();
        
        
        
    }

}
public abstract class Prioritatible
{
    public int index;
    public Prioritatible(Car[] car, int index)
    {
        this.car = car;
        this.index = index;
    }

    public Car[] car { get; set; }
    public abstract void SetPriority();

}

public class DirectionLeft : Prioritatible
{
    public DirectionLeft(Car[] car, int index) : base(car, index){}

    public override void SetPriority()
    {
        
    }
}
public class DirectionRight : Prioritatible
{
    protected DirectionRight(Car[] car, int index) : base(car, index){}

    public override void SetPriority()
    {

    }
}
public class DirectionForward : Prioritatible
{
    public DirectionForward(Car[] car, int index) : base(car, index){}
    public override void SetPriority()
    {

        for (int i = 0; i < car.Length; i++)
        {
            Position pos = car[index].Position - 1;
            if (car[i].Position == pos)
            {
                car[index].priority++;
            }
        }
        Debug.Log(car[index].priority + index.ToString());
    }
}

