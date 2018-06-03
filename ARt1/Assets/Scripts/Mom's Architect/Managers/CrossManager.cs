
using Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vuforia;

[CreateAssetMenu(fileName = "CrossManager", menuName = "Managers/CrossManager")]
class CrossManager : ManagerBase
{
    public GameObject prefabOfCross;

    public GameObject Cross { get; private set; }

    public void SetCrossGO(bool AR)
    {
        Cross = Instantiate(prefabOfCross, GameObject.Find("Static").transform, false);
        if (AR)
        {
            Cross.GetComponent<ImageTargetBehaviour>().enabled = true;
            Cross.GetComponent<DefaultInitializationErrorHandler>().enabled = true;
            Cross.GetComponent<TurnOffBehaviour>().enabled = true;
            Cross.GetComponent<DefaultTrackableEventHandler>().enabled = true;

        }
        Cross.transform.eulerAngles= new Vector3 (0, 0, 0);
    }
    public void setAngles()
    {
        Cross.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}

