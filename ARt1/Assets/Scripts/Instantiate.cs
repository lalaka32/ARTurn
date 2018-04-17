using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Random = System.Random;

class PositionRotationAnimation
{
    string nameOfPosition;
    public RuntimeAnimatorController Controller { get; set; }
    public PositionRotationAnimation(Vector3 position, Vector3 rotation,Position numberpos, RuntimeAnimatorController controller)
    {
        Controller = controller;
        Position = position;
        Rotation = rotation;
        NumberOfPosition = numberpos;
        
    }
    public void Apropriation(GameObject game)
    {
        game.name = NumberOfPosition.ToString();
        game.transform.localPosition = Position;
        game.transform.eulerAngles = Rotation;
        game.GetComponent<Car>().Position = NumberOfPosition;
        game.AddComponent<Animator>().runtimeAnimatorController = Controller;
    }
    public Vector3 Position{ get;set;}

    public Vector3 Rotation{ get;set;}

    public Position NumberOfPosition { get; set; }
}

public class Instantiate : MonoBehaviour   
{
    public bool restart;
    public GameObject prefabOfCar;
    public RuntimeAnimatorController[] controllers = new RuntimeAnimatorController[4];

    GameObject[] masCars;
    public void Restart()
    {
        restart = true;
    }
    public GameObject[] MasCars
    {
        get
        {
            return masCars;
        }

        private set
        {
            masCars = value;
        }
    }

    public TrafficLight trafficLight;
    PositionRotationAnimation[] posRotAnim = new PositionRotationAnimation[4];


    private void Awake()
    {

        trafficLight = TrafficLight.off;
    }
    private void Start()
    {
        StartCoroutine(SimpleGenerator(4f));//измени здесь для 1-ого создания

    }
    IEnumerator SimpleGenerator(float timeout)
    {
        posRotAnim[0] = new PositionRotationAnimation(new Vector3(-0.6815f, 0.0545f, -0.163f), new Vector3(0, 90, 0), Position.first, controllers[0]);
        posRotAnim[1] = new PositionRotationAnimation(new Vector3(-0.167f, 0.05449999f, 0.771f), new Vector3(0, 180, 0), Position.second, controllers[1]);
        posRotAnim[2] = new PositionRotationAnimation(new Vector3(0.754f, 0.05449999f, 0.128f), new Vector3(0, -90, 0), Position.third, controllers[2]);
        posRotAnim[3] = new PositionRotationAnimation(new Vector3(0.178f, 0.05449999f, -0.712f), new Vector3(0, 0, 0), Position.fourth, controllers[3]);
        while (true)//измени здесь для 1-ого создания
        {
            restart = false;
            Random random = new Random();
            MasCars = new GameObject[2];
            InstantiateCars();
            yield return new WaitWhile(()=> restart == false);
            
            foreach (GameObject item in MasCars)//измени здесь для 1-ого создания
            {
                Destroy(item);
            }
        }
    }

    void InstantiateCars()
    {
        Shuffle(posRotAnim);
        
        for (int i = 0; i < MasCars.Length; i++)
        {
            MasCars[i] = Instantiate(prefabOfCar, transform, false);
            MasCars[i].transform.localScale = new Vector3(0.165f, 0.165f, 0.165f);
            posRotAnim[i].Apropriation(MasCars[i]);
        }
        MasCars[0].GetComponent<MeshRenderer>().materials[0].color = new Color(2, 2, 2);//просто для обозначения плеера по цвету
        GetComponent<LOGIC_V001>().MasCars = MasCars;
    }

    void Shuffle(PositionRotationAnimation[] posRotAnim)
    {
        Random random = new Random();
        for (int i = posRotAnim.Length - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            // обменять значения data[j] и data[i]
            var temp = posRotAnim[j];
            posRotAnim[j] = posRotAnim[i];
            posRotAnim[i] = temp;
        }
    }
    void GenerationAdditionalStructures()
    {

        //есть ли светофоры, 3 варианта: красный, зелёный, мигающий жёлтый(по умолчанию)
        //есть ли знаки приоритета (нет по умолчанию)
        //
    }

}
