using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantceParticular : MonoBehaviour {
    public Timer timer;
    // Use this for initialization
    void Start () {
        ToolBox.Get<SignManager>().ClearSigns();
        ToolBox.Get<CarManager>().Clear();
        ToolBox.Get<TrafficLightManager>().Clear();

        ToolBox.Get<CrossManager>().SetCrossGO();
        ToolBox.Get<UIManager>().SetUIForRevision();
        
        ToolBox.Get<CameraManager>().SetCamGO(ToolBox.Get<SettingsPlayer>().ARCamera);
        Debug.Log(ToolBox.Get<SettingsPlayer>().numberOfRevisionQuestion+"--------------"+ ToolBox.Get<ProcessingAnsvers>().lvlSituat.Count);
        RoadSituation RS = ToolBox.Get<ProcessingAnsvers>().lvlSituat[ToolBox.Get<SettingsPlayer>().numberOfRevisionQuestion];
        ToolBox.Get<TrafficLightManager>().GenerationTrafficLight(RS, ToolBox.Get<CrossManager>().Cross.transform);

        ToolBox.Get<SignManager>().GenerationTrafficSigns(RS, ToolBox.Get<CrossManager>().Cross.transform);

        ToolBox.Get<CarManager>().GenerateCars(RS, ToolBox.Get<CrossManager>().Cross.transform);
        ToolBox.Get<UIManager>().CreateBottons(ToolBox.Get<CarManager>().MasCars.Length);
        ToolBox.Get<ProcessingAnsvers>().DebugOut(ToolBox.Get<SettingsPlayer>().numberOfRevisionQuestion);
        if (!ToolBox.Get<SettingsPlayer>().ARCamera)
        {
            ToolBox.Get<CameraManager>().SetLocation(ToolBox.Get<CarManager>().MasCars[0], new Vector3(-20, 10, 0));
        }
        timer = ToolBox.Get<TimerManager>().SetTimer(20f, delegate { SceneManager.LoadScene(3); });

    }
    void FixedUpdate()
    {
        ToolBox.Get<TimerManager>().ProsessingTimer(Time.deltaTime);
        ToolBox.Get<UIManager>().SetTimerValue(timer.TimeCount);
    }
}
