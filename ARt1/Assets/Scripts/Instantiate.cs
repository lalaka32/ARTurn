using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

class PositionAndRotation
{
    Vector3 position;
    Vector3 rotation;

    public PositionAndRotation(Vector3 position, Vector3 rotation)
    {
        Position = position;
        Rotation = rotation;
    }
    public void Apropriation(GameObject game)
    {
        game.transform.localPosition = Position;
        game.transform.eulerAngles = Rotation;
    }
    public Vector3 Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }

    public Vector3 Rotation
    {
        get
        {
            return rotation;
        }

        set
        {
            rotation = value;
        }
    }
}

public class Instantiate : MonoBehaviour   
{
   
    public GameObject Car;
    public GameObject[] masCars;

    public TrafficLight trafficLight;

    public RuntimeAnimatorController[] contollers = new RuntimeAnimatorController[4];
    NamesOfCars names;
    private void Awake()
    {
        masCars = new GameObject[2];
        trafficLight = TrafficLight.off;
    }
    private void Start()
    {
        StartCoroutine(SimpleGenerator());

    }
    IEnumerator SimpleGenerator()
    {
        yield return new WaitForEndOfFrame();
        InstantiateCarsTwo();
    }

        
    PositionAndRotation[] andRotation = {new PositionAndRotation(new Vector3(-0.6815f, 0.0545f,-0.163f),new Vector3(0, 90, 0)),
    new PositionAndRotation(new Vector3( -0.167f, 0.05449999f, 0.771f ),new Vector3(0, 180, 0)),
    new PositionAndRotation(new Vector3(0.754f, 0.05449999f, 0.128f), new Vector3(0, -90, 0)),
    new PositionAndRotation(new Vector3(0.178f, 0.05449999f, -0.712f), new Vector3(0, 0, 0)),
    };
    void InstantiateCarsTwo()
    {
        //int[] position = Randomiser();

        for (int i = 0; i < masCars.Length; i++)
        {
            
            masCars[i] = Instantiate(Car, transform, false);
            masCars[i].transform.localScale = new Vector3(0.165f, 0.165f, 0.165f);
            andRotation[i].Apropriation(masCars[i]);
            if (i == 0)
            {
                names = NamesOfCars.PlayerCar;
                masCars[i].transform.name = names.ToString();
            }
            else
            {
                names = NamesOfCars.OtherCar;
                masCars[i].transform.name = names.ToString() + i;
            }
            masCars[i].AddComponent<Animator>().runtimeAnimatorController = contollers[i];
        }
        

    }
    //int[] Randomiser() ЛАМАЕТ
    //{
    //    int[] a = new int[4];
    //    a[0] = Random.Range(1,3);
    //    for (int i = 1; i < 4;)
    //    {
    //        int num = Random.Range(1, 3); // генерируем элемент
    //        int j;
    //        // поиск совпадения среди заполненных элементов
    //        for (j = 0; j < i; j++)
    //        {
    //            if (num == a[j])
    //                break; // совпадение найдено, элемент не подходит
    //        }
    //        if (j == i)
    //        { // совпадение не найдено
    //            a[i] = num; // сохраняем элемент
    //            i++; // переходим к следующему элементу
    //        }
    //    }

    //    return a;
    //}
    void GenerationAdditionalStructures()
    {

        //есть ли светофоры, 3 варианта: красный, зелёный, мигающий жёлтый(по умолчанию)
        //есть ли знаки приоритета (нет по умолчанию)
        //
    }

}
