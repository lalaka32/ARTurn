using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProcessingAnsvers", menuName = "Managers/ProcessingAnsvers")]
public class ProcessingAnsvers:ManagerBase
{
    public List<RoadSituation> lvlSituat = new List<RoadSituation>();

	public RoadSituation AddLvlSituat(RoadSituation situa)
    {
        lvlSituat.Add(situa);
        return situa;
    }
    public RoadSituation GetSituation(int index)
    {
        return lvlSituat[index];
    }
    public void DebugOut(int index)
    {
        Debug.Log(string.Format("{0},{1},{2} -----------{3}", lvlSituat[index].VIP, lvlSituat[index].trafficLight, lvlSituat[index].trafficSign,index));
    }
}
