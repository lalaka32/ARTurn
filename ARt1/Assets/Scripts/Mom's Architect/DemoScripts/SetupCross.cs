using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Mom_s_Architect.DemoScripts
{
    //Временно отключил из-за неработающего UI
    class SetupCross : MonoBehaviour
    {
        //Здесь нужно сделать сериалазед класс который будет хранить нарши координаты
        Vector3 vectorCross = new Vector3(-210,-5, 58);
        //И нам не нужно генерировать target если у нас обычкая камера
        private void Start()
        {
            ToolBox.Get<CrossManager>().SetCrossGO(vectorCross);
        }
    }
}
