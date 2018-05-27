using Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "CarManager", menuName = "Managers/CarManager")]
class CarManager : ManagerBase
{
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

    public GameObject[] MasCars { get; private set; }


    public void InstantiateCars(PositionRotation[] posRotAnim, Transform cross)
    {
        MasCars = new GameObject[Random.Range(2, 4)];
        Shuffle(posRotAnim);

        MasCars[0] = Instantiate(prefabsOfCars[0], cross, false);
        MasCars[0].tag = "Player";
        SettingsForGOCars<Car>(MasCars[0], posRotAnim[0]);

        int IndextOffirstCar = 1;
        if (GenerateVIP(posRotAnim[1], cross))//наверн нужно делить на VIP не вип
                                    //потому что чёт много логики для 1-го менеджера
        {
            Debug.Log("VIP");
            IndextOffirstCar = 2;
        }
        for (int i = IndextOffirstCar; i < MasCars.Length; i++)
        {
            MasCars[i] = Instantiate(prefabsOfCars[Random.Range(1, prefabsOfCars.Length)], cross, false);
            MasCars[i].tag = "BotCar";
            SettingsForGOCars<Car>(MasCars[i], posRotAnim[i]);
        }

    }
    bool GenerateVIP(PositionRotation posRotAnim, Transform cross)
    {
        int chance = Random.Range(0, chanceOfVIP);
        if (chance == 0)
        {
            MasCars[1] = Instantiate(prefabsOfVIPCars[Random.Range(0, prefabsOfVIPCars.Length)], cross, false);
            MasCars[1].tag = "VIP";
            SettingsForGOCars<VIP>(MasCars[1], posRotAnim);
            SetFlashers(MasCars[1].GetComponent<VIP>());
            return true;
        }
        return false;
    }
    void SettingsForGOCars<T>(GameObject GO, PositionRotation PRA) where T : Car
    {
        GO.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        GO.AddComponent<T>();
        PRA.SetPRAatCar(GO);
        SetLight(GO.GetComponent<Car>());
    }
    void SetFlashers(VIP carVIP)
    {
        Debug.Log("VIP");
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
            case Direction.left:
                CarInctanceLight(car, new Vector3(-1.8f, -1.16f, 5.3f));
                break;
            case Direction.right:
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

