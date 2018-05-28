
using Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "CrossManager", menuName = "Managers/CrossManager")]
class CrossManager : ManagerBase, IAwake
{
    public GameObject prefabOfCross;

    public GameObject Cross { get; private set; }

    public void SetCrossGO()
    {
        Cross = Instantiate(prefabOfCross, GameObject.Find("Static").transform, false);
    }

    public void OnAwake()
    {
    }
}

