using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Instantiate : MonoBehaviour   
{
    public bool Restart { get; set; }
    public GameObject[] MasCars { get; private set; }

    Vector3 vectorCam = new Vector3(-208.1f, 82.1f, 55);
    Quaternion quaternion = Quaternion.Euler(90, 0, 0);

    private void Start()
    {
        ToolBox.Get<CrossManager>().SetCrossGO();
        ToolBox.Get<UIManager>().SetUI();
        ToolBox.Get<CameraManager>().SetCamGO(vectorCam, quaternion);
        StartCoroutine(SimpleGenerator());
    }
    
    IEnumerator SimpleGenerator()
    {
        PositionRotation[] posRotAnim = GetConstPRofCars();

        while (true)//измени здесь для 1-ого создания
        {
            Restart = false;

            ToolBox.Get<TrafficLightManager>().GenerationTrafficLight(GetConstPRofTL(), ToolBox.Get<CrossManager>().Cross.transform);
            ToolBox.Get<SignManager>().GenerationTrafficSigns(ConstSignTransform(), ToolBox.Get<CrossManager>().Cross.transform);
            ToolBox.Get<CarManager>().InstantiateCars(GetConstPRofCars(),ToolBox.Get<CrossManager>().Cross.transform);
            ToolBox.Get<UIManager>().CreateBottons(ToolBox.Get<CarManager>().MasCars.Length); 

            yield return new WaitWhile(() => Restart == false);

            ToolBox.Get<UIManager>().ClearBottons();
            ToolBox.Get<SignManager>().ClearSigns();
            foreach (GameObject item in ToolBox.Get<CarManager>().MasCars)//измени здесь для 1-ого создания
            {
                Destroy(item);
            }
            ToolBox.Get<TrafficLightManager>().Clear();
        }
    }
    PositionRotation[] ConstSignTransform()
    {
        PositionRotation[] posRotAnim = new PositionRotation[4];

        posRotAnim[0] = new PositionRotation(new Vector3(-0.4f, 0.015f, -0.4f), new Vector3(-90, 180, 0),Position.first);
        posRotAnim[1] = new PositionRotation(new Vector3(-0.4f, 0.015f, 0.4f), new Vector3(-90, -90, 0),Position.second);
        posRotAnim[2] = new PositionRotation(new Vector3(0.4f, 0.015f, 0.4f), new Vector3(-90, 0, 0),Position.third);
        posRotAnim[3] = new PositionRotation(new Vector3(0.4f, 0.015f, -0.4f), new Vector3(-90, -90, -180),Position.fourth);
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
