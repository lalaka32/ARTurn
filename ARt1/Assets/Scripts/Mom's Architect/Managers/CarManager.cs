﻿using Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "CarManager", menuName = "Managers/CarManager")]
class CarManager :ManagerBase
{
    [SerializeField]
    GameObject[] prefabsOfCars;

    public RuntimeAnimatorController controller;

    public GameObject[] MasCars { get; private set; }

    public void InstantiateCars(PositionRotation[] posRotAnim)
    {
        MasCars = new GameObject[Random.Range(2, 4)];
        Shuffle(posRotAnim);
        MasCars[0] = Instantiate(prefabsOfCars[0], GameObject.Find("ImageTarget(Clone)").transform, false);
        MasCars[0].tag = "Player";
        SettingsForGOCars(MasCars[0], posRotAnim[0]);
        for (int i = 1; i < MasCars.Length; i++)
        {
            MasCars[i] = Instantiate(prefabsOfCars[Random.Range(1, prefabsOfCars.Length)], GameObject.Find("ImageTarget(Clone)").transform, false);
            MasCars[i].tag = "BotCar";
            SettingsForGOCars(MasCars[i], posRotAnim[i]);
        }
        GameObject.Find("ImageTarget(Clone)").GetComponent<LOGIC_V001>().MasCars = MasCars;
    }

    void SettingsForGOCars(GameObject GO, PositionRotation PRA)
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
    void CarInctanceLight(Car car, Vector3 vector)
    {
        var turner = ToolBox.Get<CrossManager>().prefabOfLight;
        turner.transform.position = car.transform.GetChild(0).transform.GetChild(0).position + vector;

        Instantiate(turner, turner.transform.position, Quaternion.identity, car.transform.GetChild(0).transform.GetChild(0));
        Instantiate(turner, turner.transform.position + new Vector3(-10.5f, 0, 0), Quaternion.identity, car.transform.GetChild(0).transform.GetChild(0));
    }
    void Shuffle(PositionRotation[] posRotAnim)
    {
        for (int i = posRotAnim.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i);
            // обменять значения data[j] и data[i]
            var temp = posRotAnim[j];
            posRotAnim[j] = posRotAnim[i];
            posRotAnim[i] = temp;
        }
    }
}
