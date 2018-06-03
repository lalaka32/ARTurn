using Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "TrafficLightManager", menuName = "Managers/TrafficLightManager")]
class TrafficLightManager : ManagerBase
{
    [SerializeField]
    GameObject prefabOfTrafficLight;
    [SerializeField]
    GameObject prefabOfLight;
    [SerializeField]
    RuntimeAnimatorController controllerOfLight;

    public GameObject[] TL { get; private set; }
    public Dictionary<Position, TrafficLight> PosTL;

    void GenerateTrafficGO(PositionRotation[] PRTL, Transform parent)
    {
        TL = new GameObject[4];
        for (int i = 0; i < PRTL.Length; i++)
        {
            TL[i] = Instantiate(prefabOfTrafficLight, parent.transform, false);
            PRTL[i].SetPR(TL[i]);
        }
    }

    public void GenerationTrafficLight(RoadSituation road, Transform parent)
    {
        if (road.trafficLight != TrafficLight.Empty)
        {
            GenerateTrafficGO(road.posRotLight, parent);

            GameObject[] lights1 = new GameObject[4];
            GameObject[] lights2 = new GameObject[4];
            prefabOfLight.GetComponent<Light>().range = 5;
            for (int i = 0; i < TL.Length; i++)
            {
                lights1[i] = Instantiate(prefabOfLight, TL[i].transform, false);

                lights2[i] = Instantiate(prefabOfLight, TL[i].transform, false);
            }

            switch (road.trafficLight)//Тута сам свет наверн
            {
                case TrafficLight.Off:
                    for (int i = 0; i < TL.Length; i++)
                    {
                        lights1[i].transform.localPosition = new Vector3(0, 20, -4);
                        lights2[i].transform.localPosition = new Vector3(0, 20, 4);
                        lights1[i].GetComponent<Animator>().runtimeAnimatorController = controllerOfLight;
                        lights2[i].GetComponent<Animator>().runtimeAnimatorController = controllerOfLight;
                    }
                    break;
                case TrafficLight.Red:
                    PosTL = new Dictionary<Position, TrafficLight>();
                    for (int i = 0; i < TL.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            lights1[i].GetComponent<Light>().color = Color.red;
                            lights2[i].GetComponent<Light>().color = Color.red;
                            lights1[i].transform.localPosition = new Vector3(0, 22, -4);
                            lights2[i].transform.localPosition = new Vector3(0, 22, 4);
                            PosTL.Add((Position)i, TrafficLight.Red);
                        }
                        else
                        {
                            lights1[i].GetComponent<Light>().color = Color.green;
                            lights2[i].GetComponent<Light>().color = Color.green;
                            lights1[i].transform.localPosition = new Vector3(0, 18, -4);
                            lights2[i].transform.localPosition = new Vector3(0, 18, 4);
                            PosTL.Add((Position)i, TrafficLight.Green);
                        }
                    }
                    break;
                case TrafficLight.Green:
                    PosTL = new Dictionary<Position, TrafficLight>();
                    for (int i = 0; i < TL.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            lights1[i].GetComponent<Light>().color = Color.green;
                            lights2[i].GetComponent<Light>().color = Color.green;
                            lights1[i].transform.localPosition = new Vector3(0, 18, -4);
                            lights2[i].transform.localPosition = new Vector3(0, 18, 4);
                            PosTL.Add((Position)i, TrafficLight.Green);
                        }
                        else
                        {
                            lights1[i].GetComponent<Light>().color = Color.red;
                            lights2[i].GetComponent<Light>().color = Color.red;
                            lights1[i].transform.localPosition = new Vector3(0, 22, -4);
                            lights2[i].transform.localPosition = new Vector3(0, 22, 4);
                            PosTL.Add((Position)i, TrafficLight.Red);
                        }
                    }
                    break;
            }
        }
    }

    public void Clear()
    {
        foreach (GameObject item in TL)
        {
            Destroy(item);
        }
        PosTL = null;
    }
}

