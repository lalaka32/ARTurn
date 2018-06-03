using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using Random = UnityEngine.Random;

public class Instantiate : MonoBehaviour
{
    public bool Restart { get; set; }
    public GameObject[] MasCars { get; private set; }

    int mistakes = 0;
    public Timer timer;

    private void Start()
    {
        ToolBox.Get<CameraManager>().SetCamGO(ToolBox.Get<SettingsPlayer>().ARCamera);
        ToolBox.Get<CrossManager>().SetCrossGO(ToolBox.Get<SettingsPlayer>().ARCamera);
        ToolBox.Get<UIManager>().SetAnsverButtons();
        

        StartCoroutine(SimpleGenerator());
    }

    IEnumerator SimpleGenerator()
    {
        PositionRotation[] posRotAnim = GetConstPRofCars();
        int numberOfSituation = 0;
        ToolBox.Get<CarManager>().Clear();
        ToolBox.Get<ProcessingAnsvers>().mistakesese.Clear();
        ToolBox.Get<ProcessingAnsvers>().lvlSituat.Clear();
        while (numberOfSituation != 10 || ToolBox.Get<ProcessingAnsvers>().mistakesese.Count<2)//думаю сделать чтото вроде proseesing ansver manager
                                                       //там сделать эту карутину
        {
            Restart = false;
            RoadSituation RS = new RoadSituation(
                Random.Range(2, 4), Shuffle(GetConstPRofCars()), Convert.ToBoolean(Random.Range(0, 2)), new Direction[] {(Direction)1, (Direction)1, (Direction)1, (Direction)1/*(Direction)Random.Range(0, 3), (Direction)Random.Range(0, 3), (Direction)Random.Range(0, 3), (Direction)Random.Range(0, 3)*/ },
                (TrafficSign)Random.Range(0, 3), 4, ShaffleOdd(ConstSignTransform()),
                (TrafficLight)Random.Range(0, 4), 4, GetConstPRofTL());

            ToolBox.Get<TrafficLightManager>().GenerationTrafficLight(RS, ToolBox.Get<CrossManager>().Cross.transform);

            ToolBox.Get<SignManager>().GenerationTrafficSigns(RS, ToolBox.Get<CrossManager>().Cross.transform);

            ToolBox.Get<CarManager>().GenerateCars(RS, ToolBox.Get<CrossManager>().Cross.transform);
            ToolBox.Get<ProcessingAnsvers>().lvlSituat.Add(RS);//---------------------
            ToolBox.Get<ProcessingAnsvers>().DebugOut(numberOfSituation);
            if (!ToolBox.Get<SettingsPlayer>().ARCamera)
            {
                ToolBox.Get<CameraManager>().SetLocation(ToolBox.Get<CarManager>().MasCars[0], new Vector3(-20, 10, 0));
            }
            ToolBox.Get<UIManager>().CreateBottons(ToolBox.Get<CarManager>().MasCars.Length);

            timer = ToolBox.Get<TimerManager>().SetTimer(20f, delegate { Restart = true; });

            yield return new WaitWhile(() => Restart == false);

            ToolBox.Get<UIManager>().ClearBottons();
            ToolBox.Get<SignManager>().ClearSigns();
            ToolBox.Get<CarManager>().Clear();
            ToolBox.Get<TrafficLightManager>().Clear();
            numberOfSituation++;

        }

        ToolBox.Get<UIManager>().ShowResults();
    }
    void FixedUpdate()
    {
        ToolBox.Get<TimerManager>().ProsessingTimer(Time.deltaTime);
        ToolBox.Get<UIManager>().SetTimerValue(timer.TimeCount);
    }
    PositionRotation[] Shuffle(PositionRotation[] posRotAnim)
    {
        for (int i = posRotAnim.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i);
            // обменять значения data[j] и data[i]
            var temp = posRotAnim[j];
            posRotAnim[j] = posRotAnim[i];
            posRotAnim[i] = temp;
        }
        return posRotAnim;
    }
    PositionRotation[] ShaffleOdd(PositionRotation[] PRTS)
    {
        float rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            for (int i = 0; i < PRTS.Length; i++)
            {
                if (i % 2 != 0)
                {
                    PositionRotation temp = PRTS[i].Copy();
                    PRTS[i] = PRTS[i - 1].Copy();
                    PRTS[i - 1] = temp.Copy();
                }
            }
        }
        return PRTS;
    }
    PositionRotation[] ConstSignTransform()
    {
        PositionRotation[] posRotAnim = new PositionRotation[4];

        posRotAnim[0] = new PositionRotation(new Vector3(-0.4f, 0.015f, -0.4f), new Vector3(-90, 180, 0), Position.First);
        posRotAnim[1] = new PositionRotation(new Vector3(-0.4f, 0.015f, 0.4f), new Vector3(-90, -90, 0), Position.Second);
        posRotAnim[2] = new PositionRotation(new Vector3(0.4f, 0.015f, 0.4f), new Vector3(-90, 0, 0), Position.Third);
        posRotAnim[3] = new PositionRotation(new Vector3(0.4f, 0.015f, -0.4f), new Vector3(-90, -90, -180), Position.Fourth);
        return posRotAnim;
    }

    PositionRotation[] GetConstPRofCars()
    {
        PositionRotation[] posRotAnim = new PositionRotation[4];
        posRotAnim[0] = new PositionRotation(new Vector3(-0.6f, 0, -0.163f), new Vector3(0, 90, 0), Position.First, ToolBox.Get<CarManager>().controller);
        posRotAnim[1] = new PositionRotation(new Vector3(-0.167f, 0, 0.625f), new Vector3(0, 180, 0), Position.Second, ToolBox.Get<CarManager>().controller);
        posRotAnim[2] = new PositionRotation(new Vector3(0.6f, 0, 0.163f), new Vector3(0, -90, 0), Position.Third, ToolBox.Get<CarManager>().controller);
        posRotAnim[3] = new PositionRotation(new Vector3(0.167f, 0, -0.625f), new Vector3(0, 0, 0), Position.Fourth, ToolBox.Get<CarManager>().controller);
        return posRotAnim;
    }
    PositionRotation[] GetConstPRofTL()
    {
        PositionRotation[] posRotAnim = new PositionRotation[4];
        posRotAnim[0] = new PositionRotation(new Vector3(-0.4f, 0.015f, -0.4f), new Vector3(0, 90, 0));
        posRotAnim[1] = new PositionRotation(new Vector3(-0.4f, 0.015f, 0.4f), new Vector3(0, 0, 0));
        posRotAnim[2] = new PositionRotation(new Vector3(0.4f, 0.015f, 0.4f), new Vector3(0, 90, 0));
        posRotAnim[3] = new PositionRotation(new Vector3(0.4f, 0.015f, -0.4f), new Vector3(0, 0, 0));

        return posRotAnim;
    }
}
