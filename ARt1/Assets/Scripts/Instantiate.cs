using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Instantiate : MonoBehaviour
{
    public bool Restart { get; set; }
    public GameObject[] MasCars { get; private set; }

    int mistakes = 0;
    public Timer timer;

    private void Start()
    {
        ToolBox.Get<CrossManager>().SetCrossGO();
        ToolBox.Get<UIManager>().SetAnsverButtons();
        ToolBox.Get<CameraManager>().SetCamGO();

        StartCoroutine(SimpleGenerator());
    }

    IEnumerator SimpleGenerator()
    {
        PositionRotation[] posRotAnim = GetConstPRofCars();
        int lengthOfTest = 0;
        while (lengthOfTest != 9 || mistakes == 2)//думаю сделать чтото вроде proseesing ansver manager
            //там сделать эту карутину
        {
            Restart = false;

            ToolBox.Get<TrafficLightManager>().GenerationTrafficLight(GetConstPRofTL(), ToolBox.Get<CrossManager>().Cross.transform);
            //ToolBox.Get<SignManager>().GenerationTrafficSigns(ConstSignTransform(), ToolBox.Get<CrossManager>().Cross.transform);
            ToolBox.Get<CarManager>().InstantiateCars(GetConstPRofCars(), ToolBox.Get<CrossManager>().Cross.transform);

            ToolBox.Get<CameraManager>().SetCam3Person(ToolBox.Get<CarManager>().MasCars[0], new Vector3(-20, 10, 0), true);//должно опускаться если ар
            ToolBox.Get<UIManager>().CreateBottons(ToolBox.Get<CarManager>().MasCars.Length);
            
            timer = ToolBox.Get<TimerManager>().SetTimer(20f, delegate { Restart = true; });

            yield return new WaitWhile(() => Restart == false);

            ToolBox.Get<UIManager>().ClearBottons();
            ToolBox.Get<SignManager>().ClearSigns();
            ToolBox.Get<CarManager>().Clear();  
            ToolBox.Get<TrafficLightManager>().Clear();
            lengthOfTest++;
        }
        ToolBox.Get<UIManager>().ShowResults();
    }
    void FixedUpdate()
    {
        ToolBox.Get<TimerManager>().ProsessingTimer(Time.deltaTime);
        ToolBox.Get<UIManager>().SetTimerValue(timer.TimeCount);
    }
    PositionRotation[] ConstSignTransform()
    {
        PositionRotation[] posRotAnim = new PositionRotation[4];

        posRotAnim[0] = new PositionRotation(new Vector3(-0.4f, 0.015f, -0.4f), new Vector3(-90, 180, 0), Position.first);
        posRotAnim[1] = new PositionRotation(new Vector3(-0.4f, 0.015f, 0.4f), new Vector3(-90, -90, 0), Position.second);
        posRotAnim[2] = new PositionRotation(new Vector3(0.4f, 0.015f, 0.4f), new Vector3(-90, 0, 0), Position.third);
        posRotAnim[3] = new PositionRotation(new Vector3(0.4f, 0.015f, -0.4f), new Vector3(-90, -90, -180), Position.fourth);
        return posRotAnim;
    }

    PositionRotation[] GetConstPRofCars()
    {
        PositionRotation[] posRotAnim = new PositionRotation[4];
        posRotAnim[0] = new PositionRotation(new Vector3(-0.6f, 0, -0.163f), new Vector3(0, 90, 0), Position.first, ToolBox.Get<CarManager>().controller);
        posRotAnim[1] = new PositionRotation(new Vector3(-0.167f, 0, 0.625f), new Vector3(0, 180, 0), Position.second, ToolBox.Get<CarManager>().controller);
        posRotAnim[2] = new PositionRotation(new Vector3(0.6f, 0, 0.163f), new Vector3(0, -90, 0), Position.third, ToolBox.Get<CarManager>().controller);
        posRotAnim[3] = new PositionRotation(new Vector3(0.167f, 0, -0.625f), new Vector3(0, 0, 0), Position.fourth, ToolBox.Get<CarManager>().controller);
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
