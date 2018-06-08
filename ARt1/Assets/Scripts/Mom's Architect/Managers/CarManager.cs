using Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "CarManager", menuName = "Managers/CarManager")]
class CarManager : ManagerBase
{
    [SerializeField]
    GameObject playerCar;
    [SerializeField]
    GameObject[] prefabsOfCars;
    [SerializeField]
    GameObject[] prefabsOfVIPCars;
    [SerializeField]
    GameObject prefabOfLight;//2 раза 1 и то-же нужно что-то придумать
    [SerializeField]
    RuntimeAnimatorController controllerOfLight;
    [SerializeField]
    int chanceOfVIP;

    public RuntimeAnimatorController controller;

    public GameObject[] MasCars { get; set; }
    bool VIP;
    PositionRotation[] carPos;
    Direction[] directions;
    public GameObject[] GenerateCars(RoadSituation roadSituation, Transform cross)
    {
        MasCars = new GameObject[roadSituation.CountOfCars];
        carPos = roadSituation.posRotAnimCar;
        this.VIP = roadSituation.VIP;
        this.directions = roadSituation.directions;
        return InstantiateCars(cross);
    }

    public GameObject[] InstantiateCars( Transform cross)
    {
        MasCars[0] = Instantiate(playerCar, cross, false);
        SettingsForGOCars<Car>(MasCars[0], carPos[0], directions[0]);

        int IndextOfFirstCar = 1;
        if (VIP)
        {
            IndextOfFirstCar = 2;
            MasCars[1] = Instantiate(prefabsOfVIPCars[Random.Range(0, prefabsOfVIPCars.Length)], cross, false);
            SettingsForGOCars<VIP>(MasCars[1], carPos[1],directions[1]);
            SetFlashers(MasCars[1].GetComponent<VIP>());
        }
        for (int i = IndextOfFirstCar; i < MasCars.Length; i++)
        {
            MasCars[i] = Instantiate(prefabsOfCars[Random.Range(0, prefabsOfCars.Length)], cross, false);
            SettingsForGOCars<Car>(MasCars[i], carPos[i],directions[i]);
        }
        SetTags(VIP);
        return MasCars;
    }

    private void SetTags(bool VIP)
    {
        MasCars[0].tag = "Player";
        if (VIP)
        {
            MasCars[1].tag = "VIP";
        }
        foreach (GameObject car in MasCars)
        {
            if (car.tag == "Untagged")
            {
                car.tag = "BotCar";
            }
        }
    }

    void SettingsForGOCars<T>(GameObject GO, PositionRotation PRA, Direction direction) where T : Car
    {
        GO.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        GO.AddComponent<T>();
        GO.GetComponent<T>().Direction = direction;
        PRA.SetPRAatCar(GO);
        SetLight(GO.GetComponent<Car>());
    }
    void SetFlashers(VIP carVIP)
    {
        Vector3 lightPosition = carVIP.transform.GetChild(0).transform.GetChild(0).position + new Vector3(0, 3, 0);
        GameObject flashers = Instantiate(prefabOfLight, lightPosition, Quaternion.identity, carVIP.transform.GetChild(0).transform.GetChild(0));
        flashers.GetComponent<Light>().type = LightType.Point;
        flashers.GetComponent<Light>().color = Color.red;
        flashers.GetComponent<Light>().range = 10;
    }
    void SetLight(Car car)
    {
        switch (car.Direction)
        {
            case Direction.Left:
                CarInctanceLight(car, new Vector3(-1.8f, -1.16f, 5.3f));
                break;
            case Direction.Right:
                CarInctanceLight(car, new Vector3(1.8f, -1.16f, 5.3f));
                break;
        }
    }
    void CarInctanceLight(Car car, Vector3 vector)
    {
        GameObject turner = prefabOfLight;
        turner.transform.position = car.transform.GetChild(0).transform.GetChild(0).position;
        turner.transform.localPosition += (vector.z * car.transform.forward) + (vector.x * car.transform.right) + (vector.y * Vector3.up);
        Instantiate(turner, turner.transform.position, Quaternion.identity, car.transform.GetChild(0).transform.GetChild(0)).GetComponent<Animator>().runtimeAnimatorController = controllerOfLight;
        Instantiate(turner, turner.transform.position + (car.transform.forward * -10.5f), Quaternion.identity, car.transform.GetChild(0).transform.GetChild(0)).GetComponent<Animator>().runtimeAnimatorController = controllerOfLight;
    }
    public void Clear()
    {
        if (MasCars !=null)
        {
            foreach (GameObject item in ToolBox.Get<CarManager>().MasCars)
            {
                Destroy(item);
            }
        }

    }
}

