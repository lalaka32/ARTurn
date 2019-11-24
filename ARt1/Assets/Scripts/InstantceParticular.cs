using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class InstantceParticular : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public Timer timer;
    // Use this for initialization
    void Start()
    {
        ToolBox.Get<CameraManager>().SetCamGO(ToolBox.Get<SettingsPlayer>().ARCamera);
        ToolBox.Get<CrossManager>().SetCrossGO(ToolBox.Get<SettingsPlayer>().ARCamera);
        if (ToolBox.Get<SettingsPlayer>().ARCamera)
        {
            mTrackableBehaviour = ToolBox.Get<CrossManager>().Cross.GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }
        else
        {
            ToolBox.Get<UIManager>().SetAnsverButtons();
            InctanceRoadSituation();
        }


    }

    private void InctanceRoadSituation()
    {
        ToolBox.Get<SignManager>().ClearSigns();
        ToolBox.Get<CarManager>().Clear();
        ToolBox.Get<TrafficLightManager>().Clear();


        ToolBox.Get<UIManager>().SetUIForRevision();



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
        timer = ToolBox.Get<TimerManager>().SetTimer(20f, delegate { ToolBox.Get<UIManager>().ShowResults(); });
    }

    void FixedUpdate()
    {
        if (!ToolBox.Get<TimerManager>().IsNull())
        {
            ToolBox.Get<TimerManager>().ProsessingTimer(Time.deltaTime);
            ToolBox.Get<UIManager>().SetTimerValue(timer.TimeCount);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        ToolBox.Get<CrossManager>().setAngles();
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
        newStatus == TrackableBehaviour.Status.TRACKED ||
        newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            ToolBox.Get<UIManager>().SetAnsverButtons();
            InctanceRoadSituation();
        }
        else if (previousStatus == TrackableBehaviour.Status.DETECTED ||
        previousStatus == TrackableBehaviour.Status.TRACKED ||
        previousStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            ToolBox.Get<UIManager>().ClearPrefab();
            ToolBox.Get<SignManager>().ClearSigns();
            ToolBox.Get<CarManager>().Clear();
            ToolBox.Get<TrafficLightManager>().Clear();
            timer.Stop();
        }
    }
}
