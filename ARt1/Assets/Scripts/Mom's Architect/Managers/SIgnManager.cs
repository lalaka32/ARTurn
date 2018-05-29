using Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "SignManager", menuName = "Managers/SignManager")]
class SignManager : ManagerBase
{
    [SerializeField]
    GameObject prefabOfMain;
    [SerializeField]
    GameObject prefabOfSecondary;
    [SerializeField]
    GameObject prefabOfStop;

    public GameObject[] signArray;
    public Dictionary<Position, TrafficSign> TS;

    void InstantiateTrafficSign(PositionRotation[] PRTL,int Count, Transform parent)
    {
        signArray = new GameObject[Count];
        for (int i = 0; i < Count; i++)
        {
            if (i % 2 == 0)
            {
                signArray[i] = Instantiate(prefabOfMain, parent.transform, false);
                TS.Add(PRTL[i].NumberOfPosition, TrafficSign.main);
            }
            else
            {
                signArray[i] = Instantiate(prefabOfSecondary, parent.transform, false);
                TS.Add(PRTL[i].NumberOfPosition, TrafficSign.secondary);
            }
            PRTL[i].SetPR(signArray[i]);
        }
    }

    public void GenerationTrafficSigns(RoadSituation road, Transform parent)
    {
        if (road.trafficSign != TrafficSign.empty)
        {
            TS = new Dictionary<Position, TrafficSign>(3);

            InstantiateTrafficSign(road.posRotSign,road.CoutOfSigns, parent);
        }
    }
    public void ClearSigns()
    {
        TS = null;
        foreach (GameObject sign in signArray)
        {
            Destroy(sign);
        }
    }
}
