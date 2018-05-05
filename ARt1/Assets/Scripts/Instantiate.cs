using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Instantiate : MonoBehaviour   
{
    public GameObject[] prefabOfCar;
    public RuntimeAnimatorController[] controllers = new RuntimeAnimatorController[4];

    GameObject[] masCars;
    public bool Restart { get; set; }
    public GameObject canvas;
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
        StartCoroutine(SimpleGenerator());//измени здесь для 1-ого создания

    }
    IEnumerator SimpleGenerator()
    {
        posRotAnim[0] = new PositionRotationAnimation(new Vector3(-0.6815f, 0, -0.163f), new Vector3(0, 90, 0), Position.first, controllers[0]);
        posRotAnim[1] = new PositionRotationAnimation(new Vector3(-0.167f, 0, 0.771f), new Vector3(0, 180, 0), Position.second, controllers[1]);
        posRotAnim[2] = new PositionRotationAnimation(new Vector3(0.754f, 0, 0.128f), new Vector3(0, -90, 0), Position.third, controllers[2]);
        posRotAnim[3] = new PositionRotationAnimation(new Vector3(0.178f, 0, -0.712f), new Vector3(0, 0, 0), Position.fourth, controllers[3]);
        while (true)//измени здесь для 1-ого создания
        {
            Restart = false;
            Random random = new Random();
            MasCars = new GameObject[Random.Range(2,4)];
            InstantiateCars();
            canvas = GameObject.Find("Canvas");
            canvas.GetComponent<InstantiateBottons>().CreateBottons(MasCars.Length);
            yield return new WaitWhile(()=> Restart == false);
            canvas.GetComponent<InstantiateBottons>().Clear();
            foreach (GameObject item in MasCars)//измени здесь для 1-ого создания
            {
                Destroy(item);
            }
        }
    }
    void SetPefabs(GameObject obg)
    {

    }
    void InstantiateCars()
    {
        Shuffle(posRotAnim);
        MasCars[0] = Instantiate(prefabOfCar[0], transform, false);
        for (int i = 0; i < MasCars.Length; i++)
        {
            if (i != 0)
            {
                MasCars[i] = Instantiate(prefabOfCar[Random.Range(1, prefabOfCar.Length)], transform, false);
            }
            SettingsForObjects(MasCars[i]);
            posRotAnim[i].Apropriation(MasCars[i]);

        }
        //просто для обозначения плеера по цвету
        GetComponent<LOGIC_V001>().MasCars = MasCars;
    }

    private void SettingsForObjects(GameObject GO)
    {
        GO.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        GO.AddComponent<Car>();
        
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
