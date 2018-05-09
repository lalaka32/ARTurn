using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Instantiate : MonoBehaviour   
{
    public GameObject[] prefabOfCar;
    [SerializeField]
    RuntimeAnimatorController[] controllers = new RuntimeAnimatorController[4];
    public bool Restart { get; set; }
    public GameObject[] MasCars { get; private set; }
    public TrafficLight trafficLight;
    PositionRotationAnimation[] posRotAnim = new PositionRotationAnimation[4];
    [SerializeField]
    GameObject canvas;

    private void Awake()
    {
        trafficLight = TrafficLight.off;
    }
    private void Start()
    {
        GetConstPRA();
        StartCoroutine(SimpleGenerator());
    }
    IEnumerator SimpleGenerator()
    {
        while (true)//измени здесь для 1-ого создания
        {
            Restart = false;

            GenerationAdditionalStructures();
            InstantiateCars();
            canvas.GetComponent<InstantiateBottons>().CreateBottons(MasCars.Length);

            yield return new WaitWhile(() => Restart == false);

            canvas.GetComponent<InstantiateBottons>().Clear();
            foreach (GameObject item in MasCars)//измени здесь для 1-ого создания
            {
                Destroy(item);
            }
        }
    }

    private void GetConstPRA()
    {
        posRotAnim[0] = new PositionRotationAnimation(new Vector3(-0.6815f, 0, -0.163f), new Vector3(0, 90, 0), Position.first, controllers[0]);
        posRotAnim[1] = new PositionRotationAnimation(new Vector3(-0.167f, 0, 0.771f), new Vector3(0, 180, 0), Position.second, controllers[1]);
        posRotAnim[2] = new PositionRotationAnimation(new Vector3(0.754f, 0, 0.128f), new Vector3(0, -90, 0), Position.third, controllers[2]);
        posRotAnim[3] = new PositionRotationAnimation(new Vector3(0.178f, 0, -0.712f), new Vector3(0, 0, 0), Position.fourth, controllers[3]);
    }

    void InstantiateCars()
    {
        MasCars = new GameObject[Random.Range(2, 4)];
        Shuffle(posRotAnim);
        MasCars[0] = Instantiate(prefabOfCar[0], transform, false);
        MasCars[0].tag = "Player";
        SettingsForObjects(MasCars[0], posRotAnim[0]);
        for (int i = 1; i < MasCars.Length; i++)
        {
            MasCars[i] = Instantiate(prefabOfCar[Random.Range(1, prefabOfCar.Length)], transform, false);
            MasCars[i].tag =  "BotCar";
            SettingsForObjects(MasCars[i], posRotAnim[i]);
        }
        GetComponent<LOGIC_V001>().MasCars = MasCars;
    }

    private void SettingsForObjects(GameObject GO,PositionRotationAnimation PRA)
    {
        GO.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        GO.AddComponent<Car>();
        SetLight(GO.GetComponent<Car>());
        PRA.Apropriation(GO);
    }
    void SetLight(Car car)
    {
        var light1 = new GameObject("light");
        light1.transform.parent = car.transform.Find("Body").transform;
        light1.AddComponent<Light>().color = Color.yellow;
        light1.GetComponent<Light>().intensity = 30;
        switch (car.Direction)
        {
            case Direction.left:
                light1.transform.position = car.transform.Find("Body").transform.position + new Vector3(-4, 0, 4);
                Instantiate(light1, light1.transform.position + new Vector3(0, 0, -10), Quaternion.identity, car.transform.Find("Body").transform);
                break;
            case Direction.right:
                light1.transform.position = car.transform.Find("Body").transform.position + new Vector3(4, 0, 4);
                Instantiate(light1, light1.transform.position + new Vector3(0, 0, -10), Quaternion.identity, car.transform.Find("Body").transform);
                break;
        }
    }
    void Shuffle(PositionRotationAnimation[] posRotAnim)
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

        //есть ли светофоры, 3 варианта: красный, зелёный, мигающий жёлтый(по умолчанию)
        //есть ли знаки приоритета (нет по умолчанию)
        //
    }
}
