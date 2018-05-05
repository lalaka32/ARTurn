﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName ="CameraManager",menuName = "Managers/CameraManager")]
class CameraManager:ManagerBase, IAwake
{
    public GameObject camGO;

    public void SetCamGO(Vector3 pos)
    {
        Instantiate(camGO, pos,Quaternion.identity);
    }
    public void OnAwake()
    {
        //Должно быть считывание с настроек какую камеру создац
        //Кароч в настройках будет галочка типо ар или обчная камера
        //Если AR то в SetCamGO кам накинуть просто скрипты
        //Это поможет не хранить 2 префаба(3 килобайта забей)
    }

}
