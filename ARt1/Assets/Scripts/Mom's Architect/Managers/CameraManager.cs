﻿using BeeFly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName ="CameraManager",menuName = "Managers/CameraManager")]
class CameraManager:ManagerBase, IAwake
{
    [SerializeField]
    GameObject camPrefab;

    public GameObject MainCamera { get; private set; }

    public void SetCamGO(Vector3 pos,Quaternion quaternion,bool sneaking=false)
    {
        MainCamera = Instantiate(camPrefab, pos, quaternion);
        MainCamera.transform.parent = GameObject.Find("Cameras").transform;
        
    }
    
    public void OnAwake()
    {
        //Должно быть считывание с настроек какую камеру создац
        //Кароч в настройках будет галочка типо ар или обчная камера
        //Если AR то в SetCamGO кам накинуть просто скрипты
        //Это поможет не хранить 2 префаба(3 килобайта забей)
    }

}
