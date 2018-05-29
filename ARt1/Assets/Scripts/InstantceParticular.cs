using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantceParticular : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ToolBox.Get<CrossManager>().SetCrossGO();
        ToolBox.Get<UIManager>().SetUIForRevision();
        ToolBox.Get<CameraManager>().SetCamGO(ToolBox.Get<SettingsPlayer>().ARCamera);
        Debug.Log(ToolBox.Get<SettingsPlayer>().numberOfRevisionQuestion - 2+"--------------"+ ToolBox.Get<ProcessingAnsvers>().lvlSituat.Count);
        RoadSituation RS = ToolBox.Get<ProcessingAnsvers>().lvlSituat[ToolBox.Get<SettingsPlayer>().numberOfRevisionQuestion-1];
        ToolBox.Get<TrafficLightManager>().GenerationTrafficLight(RS, ToolBox.Get<CrossManager>().Cross.transform);

        ToolBox.Get<SignManager>().GenerationTrafficSigns(RS, ToolBox.Get<CrossManager>().Cross.transform);

        ToolBox.Get<CarManager>().GenerateCars(RS, ToolBox.Get<CrossManager>().Cross.transform);
        if (!ToolBox.Get<SettingsPlayer>().ARCamera)
        {
            ToolBox.Get<CameraManager>().SetLocation(ToolBox.Get<CarManager>().MasCars[0], new Vector3(-20, 10, 0));
        }
    }
	
}
