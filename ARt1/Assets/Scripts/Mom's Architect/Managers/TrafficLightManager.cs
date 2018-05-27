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
    TrafficLight trafficLight;
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
    public void GenerationTrafficLight(PositionRotation[] PRTL, Transform parent)
    {
        trafficLight = (TrafficLight)Random.Range(0, 4);
        //trafficLight = TrafficLight.red;
        Debug.Log(trafficLight);
        if (trafficLight != TrafficLight.empty)
        {
            GenerateTrafficGO(PRTL, parent);

            GameObject[] lights1 = new GameObject[4];
            GameObject[] lights2 = new GameObject[4];
            prefabOfLight.GetComponent<Light>().range = 5;
            for (int i = 0; i < TL.Length; i++)
            {
                lights1[i] = Instantiate(prefabOfLight, TL[i].transform, false);

                lights2[i] = Instantiate(prefabOfLight, TL[i].transform, false);

            }

            switch (trafficLight)//Тута сам свет наверн
            {
                case TrafficLight.off:
                    for (int i = 0; i < TL.Length; i++)
                    {
                        lights1[i].transform.localPosition = new Vector3(0, 20, -4);
                        lights2[i].transform.localPosition = new Vector3(0, 20, 4);
                        lights1[i].GetComponent<Animator>().runtimeAnimatorController = controllerOfLight;
                        lights2[i].GetComponent<Animator>().runtimeAnimatorController = controllerOfLight;
                    }
                    break;
                case TrafficLight.red:
                    PosTL = new Dictionary<Position, TrafficLight>(3);
                    for (int i = 0; i < TL.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            lights1[i].GetComponent<Light>().color = Color.red;
                            lights2[i].GetComponent<Light>().color = Color.red;
                            PosTL.Add((Position)i, TrafficLight.red);
                        }
                        else
                        {
                            lights1[i].GetComponent<Light>().color = Color.green;
                            lights2[i].GetComponent<Light>().color = Color.green;
                            PosTL.Add((Position)i, TrafficLight.green);
                        }

                        lights1[i].transform.localPosition = new Vector3(0, 22, -4);

                        lights2[i].transform.localPosition = new Vector3(0, 22, 4);
                    }
                   
                    break;
                case TrafficLight.green:
                    PosTL = new Dictionary<Position, TrafficLight>(3);
                    for (int i = 0; i < TL.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            lights1[i].GetComponent<Light>().color = Color.green;
                            lights2[i].GetComponent<Light>().color = Color.green;
                            PosTL.Add((Position)i, TrafficLight.green);
                        }
                        else
                        {
                            lights1[i].GetComponent<Light>().color = Color.red;
                            lights2[i].GetComponent<Light>().color = Color.red;
                            PosTL.Add((Position)i, TrafficLight.red);
                        }
                        lights1[i].transform.localPosition = new Vector3(0, 18, -4);
                        lights2[i].transform.localPosition = new Vector3(0, 18, 4);
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

