using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Instantiate : MonoBehaviour   
{
    public GameObject[] prefabOfCar;
    public RuntimeAnimatorController controller = new RuntimeAnimatorController();
    public bool Restart { get; set; }
    public GameObject[] MasCars { get; private set; }
    public GameObject[] TL { get; private set; }
    public TrafficLight trafficLight;
    public Dictionary<Position, TrafficLight> PosTL;

    [SerializeField]
    GameObject TrafficLightGO;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject light1;

    private void Start()
    {
        StartCoroutine(SimpleGenerator());
    }
    IEnumerator SimpleGenerator()
    {
        PositionRotation[] posRotAnim = GetConstPRofCars();

        while (true)//измени здесь для 1-ого создания
        {
            Restart = false;

            GenerationAdditionalStructures();
            InstantiateCars(posRotAnim);
            canvas.GetComponent<InstantiateBottons>().CreateBottons(MasCars.Length);

            yield return new WaitWhile(() => Restart == false);

            canvas.GetComponent<InstantiateBottons>().Clear();
            foreach (GameObject item in MasCars)//измени здесь для 1-ого создания
            {
                Destroy(item);
            }
            foreach (GameObject item in TL)
            {
                Destroy(item);
            }
        }
    }

    PositionRotation[] GetConstPRofCars()
    {
        PositionRotation[] posRotAnim = new PositionRotation[4];
        posRotAnim[0] = new PositionRotation(new Vector3(-0.6f, 0, -0.163f), new Vector3(0, 90, 0), Position.first, controller);
        posRotAnim[1] = new PositionRotation(new Vector3(-0.167f, 0, 0.625f), new Vector3(0, 180, 0), Position.second, controller);
        posRotAnim[2] = new PositionRotation(new Vector3(0.6f, 0, 0.163f), new Vector3(0, -90, 0), Position.third, controller);
        posRotAnim[3] = new PositionRotation(new Vector3(0.167f, 0, -0.625f), new Vector3(0, 0, 0), Position.fourth, controller);
        return posRotAnim;
    }

    void InstantiateCars(PositionRotation[] posRotAnim)
    {
        MasCars = new GameObject[Random.Range(2, 4)];
        Shuffle(posRotAnim);
        MasCars[0] = Instantiate(prefabOfCar[0], transform, false);
        MasCars[0].tag = "Player";
        SettingsForGOCars(MasCars[0], posRotAnim[0]);
        for (int i = 1; i < MasCars.Length; i++)
        {
            MasCars[i] = Instantiate(prefabOfCar[Random.Range(1, prefabOfCar.Length)], transform, false);
            MasCars[i].tag =  "BotCar";
            SettingsForGOCars(MasCars[i], posRotAnim[i]);
        }
        GetComponent<LOGIC_V001>().MasCars = MasCars;
    }

    void SettingsForGOCars(GameObject GO,PositionRotation PRA)
    {
        GO.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        GO.AddComponent<Car>();
        SetLight(GO.GetComponent<Car>());
        PRA.SetPRAatCar(GO);
    }
    void SetLight(Car car)
    {
        switch (car.Direction)
        {
            case Direction.left:
                CarInctanceLight(car, new Vector3(5.3f, -1.16f, 1.8f));
                break;
            case Direction.right:
                CarInctanceLight(car, new Vector3(5.3f, -1.16f, -1.8f));
                break;
        }
    }
    void CarInctanceLight(Car car,Vector3 vector)
    {
        light1.transform.position = car.transform.GetChild(0).transform.GetChild(0).position + vector;

        Instantiate(light1, light1.transform.position, Quaternion.identity, car.transform.GetChild(0).transform.GetChild(0));
        Instantiate(light1, light1.transform.position + new Vector3(-10.5f, 0, 0), Quaternion.identity, car.transform.GetChild(0).transform.GetChild(0));
    }
    void Shuffle(PositionRotation[] posRotAnim)
    {
        for (int i = posRotAnim.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0,i);
            // обменять значения data[j] и data[i]
            var temp = posRotAnim[j];
            posRotAnim[j] = posRotAnim[i];
            posRotAnim[i] = temp;
        }
    }
    void GenerationAdditionalStructures()
    {
        trafficLight = (TrafficLight)Random.Range(0, 3);
        print(trafficLight);
        GenerateTraffic(GetConstPRofTL());
        switch (trafficLight)//Тута сам свет наверн
        {
            case TrafficLight.off:
                break;
            case TrafficLight.red:
                break;
            case TrafficLight.green:
                break;
            default:
                break;
        }
    }
    void GenerateTraffic(PositionRotation[] PRTL)
    {
        TL = new GameObject[4];
        for (int i = 0; i < PRTL.Length; i++)
        {
            TL[i] = Instantiate(TrafficLightGO, transform.Find("Cross"), false);
            PRTL[i].SetPR(TL[i]);
        }
    }
    PositionRotation[] GetConstPRofTL()
    {
        PositionRotation[] posRotAnim = new PositionRotation[4];
        posRotAnim[0] = new PositionRotation(new Vector3(-0.4f, 0.015f, 0.4f), new Vector3(0, 0, 0));
        posRotAnim[1] = new PositionRotation(new Vector3(0.4f, 0.015f, 0.4f), new Vector3(0, 90, 0));
        posRotAnim[2] = new PositionRotation(new Vector3(0.4f, 0.015f, -0.4f), new Vector3(0, 0, 0));
        posRotAnim[3] = new PositionRotation(new Vector3(-0.4f, 0.015f, -0.4f), new Vector3(0, 90, 0));
        return posRotAnim;
    }
}
