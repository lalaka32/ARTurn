using BeeFly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraManager", menuName = "Managers/CameraManager")]
class CameraManager : ManagerBase
{
    [SerializeField]
    GameObject usialCamPrefab;
    [SerializeField]
    GameObject ARCam;

    public GameObject MainCamera { get; private set; }

    public void SetCamGO(bool AR)
    {
        if (AR)
        {
            MainCamera = Instantiate(ARCam, GameObject.Find("Cameras").transform);
        }
        else
        {
            
            MainCamera = Instantiate(usialCamPrefab, GameObject.Find("Cameras").transform);
        }
    }

    public void SetLocation(GameObject gameObject, Vector3 vector3, bool v)
    {
        Vector3 backVector = gameObject.transform.forward * vector3.x;
        MainCamera.transform.position = gameObject.transform.position + backVector + (vector3.y * Vector3.up);
        MainCamera.transform.eulerAngles = gameObject.transform.localEulerAngles;
    }
}
