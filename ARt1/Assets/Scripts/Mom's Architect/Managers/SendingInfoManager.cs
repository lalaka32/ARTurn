using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "SendingInfoManager", menuName = "Managers/SendingInfoManager")]
class SendingInfoManager : ManagerBase
{

    WebRequesting webRequesting;
    private string BASE_URL = "http://localhost:2054/api/testResult/";

    //use ToolBox.Get<ProcessingAnsvers>().lvlSituat, ToolBox.Get<ProcessingAnsvers>().mistakesese
    public void SendTest()
    {
        webRequesting = GameObject.Find("WebRequesting").GetComponent<WebRequesting>();

        var situations = ToolBox.Get<ProcessingAnsvers>().lvlSituat;
        var mistakesese = ToolBox.Get<ProcessingAnsvers>().mistakesese;

        List<RoadSituationModel> situationModels = new List<RoadSituationModel>();

        for (int i = 0; i < situations.Count; i++)
        {
            RoadSituationModel model = new RoadSituationModel();
            model.HasVip = situations[i].VIP;

            model.HasTrafficLight = (situations[i].trafficLight == TrafficLight.Empty || situations[i].trafficLight == TrafficLight.Off) ? false : true;

            model.HasSigns = (situations[i].trafficSign == TrafficSign.Empty) ? false : true;

            model.Success = (mistakesese.Contains(i)) ? false : true;
            
            situationModels.Add(model);
        }
        DrivingTestModel drivingTestModel = new DrivingTestModel();
        drivingTestModel.roadSituations = situationModels;

        drivingTestModel.success = (mistakesese.Count >= 2) ? false : true;

        string jsonDrivingTestModel = JsonUtility.ToJson(drivingTestModel);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonDrivingTestModel.ToCharArray());

        webRequesting.StartSendTest(BASE_URL + "post", byteData);
    }
}
