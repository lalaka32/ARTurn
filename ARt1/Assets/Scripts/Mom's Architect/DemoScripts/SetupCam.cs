using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BeeFly
{
    class SetupCam:MonoBehaviour
    {
        //Здесь нужно сделать сериалазед класс который будет хранить нарши координаты

        //Тут будут наши поведения
        //private List<>

        //Vector3 vectorCam = new Vector3(-208.1f, 82.1f, 55);
        //Quaternion quaternion = Quaternion.Euler(90,0,0);
        
        //public void Sneaking(Vector3 location)
        //{
        //    location.transform.LookAt(GameObject.FindGameObjectsWithTag("Player")[0].transform);
        //}
        private void Start()
        {
            //ToolBox.Get<CameraManager>().SetCamGO(vectorCam, quaternion); 
        }
        private void Update()
        {
            //Ну тут нужн овызывать все наши поведения
            //ТАк можно легко отрубать и врубать разные -"-

        }
    }
}
