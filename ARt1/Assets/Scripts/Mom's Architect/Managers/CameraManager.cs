using BeeFly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraManager", menuName = "Managers/CameraManager")]
class CameraManager : ManagerBase, IAwake
{
    [SerializeField]
    GameObject camPrefab;

    public GameObject MainCamera { get; private set; }

    public void SetCamGO()
    {
        MainCamera = Instantiate(camPrefab, GameObject.Find("Cameras").transform);
        //MainCamera.SetActive(false);//такое потому что если камера AR то смысла её выключать нет
    }
    public void SetCam3Person(GameObject sneakingGO, Vector3 dictance, bool sneaking = false)
    {
        //MainCamera.SetActive(true);
        Vector3 backVector = sneakingGO.transform.forward * dictance.x;

        MainCamera.transform.position = sneakingGO.transform.position + backVector + (dictance.y * Vector3.up);

        MainCamera.transform.eulerAngles = sneakingGO.transform.localEulerAngles;
    }

    public void OnAwake()
    {
        //Должно быть считывание с настроек какую камеру создац
        //Кароч в настройках будет галочка типо ар или обчная камера
        //Если AR то в SetCamGO кам накинуть просто скрипты
        //Это поможет не хранить 2 префаба(3 килобайта забей)
    }

}
