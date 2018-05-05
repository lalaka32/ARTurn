using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Mom_s_Architect.DemoScripts
{
    class SetupCam:MonoBehaviour
    {
        //Здесь нужно сделать сериалазед класс который будет хранить нарши координаты
        Vector3 vectorCam = new Vector3(-218, 12,-14);
        private void Start()
        {
            ToolBox.Get<CameraManager>().SetCamGO(vectorCam); 
        }
    }
}
