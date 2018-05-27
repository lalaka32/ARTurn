﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class Starter: MonoBehaviour{
    //Точка входа  в прогу
    //Будет просто содержать список наших систем

    public List<ManagerBase> managers = new List<ManagerBase>();

    private void Awake()
    {
        foreach (ManagerBase item in managers)
        {
            ToolBox.Add(item);
        }
        SceneManager.LoadScene(1);
    }
}
