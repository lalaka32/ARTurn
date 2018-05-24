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

    TrafficSign sign;

    public GameObject[] signArray;
    public Dictionary<Position, TrafficSign> TS;

    void GenerateTrafficSignGO(PositionRotation[] PRTL, Transform parent)
    {
        signArray = new GameObject[4];
        for (int i = 0; i < PRTL.Length; i++)
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
    public void ClearSigns()
    {
        TS = null;
        foreach (GameObject sign in signArray)
        {
            Destroy(sign);
        }
    }
    public void GenerationTrafficSigns(PositionRotation[] PRTS, Transform parent)
    {
        sign = (TrafficSign)Random.Range(0, 3);
        if (sign != TrafficSign.empty)
        {
            TS = new Dictionary<Position, TrafficSign>(3);
            Shuffle(PRTS);
            GenerateTrafficSignGO(PRTS, parent);
        }
        

    }
    void Shuffle(PositionRotation[] posRotAnim)//Хз пока тут оставлю(2 раза повтаряю)
    {
        for (int i = posRotAnim.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i);
            // обменять значения data[j] и data[i]
            var temp = posRotAnim[j];
            posRotAnim[j] = posRotAnim[i];
            posRotAnim[i] = temp;
        }
    }
}
