﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Random = System.Random;

class PositionAndRotation
{
    Vector3 position;
    Vector3 rotation;
    string nameOfPosition;
    public RuntimeAnimatorController Controller { get; set; }
    public PositionAndRotation(Vector3 position, Vector3 rotation,string nameOfPosition, RuntimeAnimatorController controller)
    {
        this.Controller = controller;
        Position = position;
        Rotation = rotation;
        this.nameOfPosition = nameOfPosition;
    }
    public void Apropriation(GameObject game)
    {
        game.transform.localPosition = Position;
        game.transform.eulerAngles = Rotation;
        game.name = nameOfPosition;
        game.AddComponent<Animator>().runtimeAnimatorController = Controller;
    }
    public Vector3 Position{ get;set;}

    public Vector3 Rotation{ get;set;}
}

public class Instantiate : MonoBehaviour   
{
   
    public GameObject Car;
    public GameObject[] masCars;

    public TrafficLight trafficLight;

    public RuntimeAnimatorController[] controllers = new RuntimeAnimatorController[4];
    PositionAndRotation[] andRotation = new PositionAndRotation[4];
    private void Awake()
    {

        trafficLight = TrafficLight.off;
    }
    private void Start()
    {
        
        StartCoroutine(SimpleGenerator(0.1f));//измени здесь для 1-ого создания

    }
    IEnumerator SimpleGenerator(float timeout)
    {
        andRotation[0] = new PositionAndRotation(new Vector3(-0.6815f, 0.0545f, -0.163f), new Vector3(0, 90, 0), "p1", controllers[0]);
        andRotation[1] = new PositionAndRotation(new Vector3(-0.167f, 0.05449999f, 0.771f), new Vector3(0, 180, 0), "p2", controllers[1]);
        andRotation[2] = new PositionAndRotation(new Vector3(0.754f, 0.05449999f, 0.128f), new Vector3(0, -90, 0), "p3", controllers[2]);
        andRotation[3] = new PositionAndRotation(new Vector3(0.178f, 0.05449999f, -0.712f), new Vector3(0, 0, 0), "p4", controllers[3]);
        while (true)//измени здесь для 1-ого создания
        {
            Random random = new Random();
            masCars = new GameObject[2];
            InstantiateCars();

            yield return new WaitForSeconds(timeout);
            
            foreach (GameObject item in masCars)//измени здесь для 1-ого создания
            {
                Destroy(item);
            }
        }
    }

    void InstantiateCars()
    {
        Shuffle(andRotation);
        
        for (int i = 0; i < masCars.Length; i++)
        {
            
            masCars[i] = Instantiate(Car, transform, false);
            masCars[0].GetComponent<MeshRenderer>().materials[0].color = new Color(2, 2 ,2);//просто для обозначения плеера по цвету
           
            masCars[i].transform.localScale = new Vector3(0.165f, 0.165f, 0.165f);
            andRotation[i].Apropriation(masCars[i]);
        }
        

    }
    PositionAndRotation[] Shuffle(PositionAndRotation[] andRotation)
    {
        Random random = new Random();
        for (int i = andRotation.Length - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            // обменять значения data[j] и data[i]
            var temp = andRotation[j];
            andRotation[j] = andRotation[i];
            andRotation[i] = temp;
        }
        return andRotation;
    }
    void GenerationAdditionalStructures()
    {

        //есть ли светофоры, 3 варианта: красный, зелёный, мигающий жёлтый(по умолчанию)
        //есть ли знаки приоритета (нет по умолчанию)
        //
    }

}
