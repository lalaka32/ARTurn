
using Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "CrossManager", menuName = "Managers/CrossManager")]
class CrossManager : ManagerBase, IAwake
{
    public GameObject prefabOfCross;
    public GameObject prefabOfTrafficLight;
    public GameObject prefabOfLight;
    public RuntimeAnimatorController controllerOfLight;


    public GameObject Cross { get; private set; }
    public GameObject[] TL { get; private set; }
    public TrafficLight trafficLight;


    public void SetCrossGO()
    {
        Cross = Instantiate(prefabOfCross,GameObject.Find("Static").transform,false);
    }

    void GenerateTraffic(PositionRotation[] PRTL)
    {
        TL = new GameObject[4];
        for (int i = 0; i < PRTL.Length; i++)
        {
            TL[i] = Instantiate(prefabOfTrafficLight, Cross.transform, false);
            PRTL[i].SetPR(TL[i]);
        }
    }
    public void GenerationAdditionalStructures(PositionRotation[] PRTL)
    {
        trafficLight = (TrafficLight)Random.Range(0, 3);
        //trafficLight = TrafficLight.red;
        Debug.Log(trafficLight);
        if (trafficLight != TrafficLight.empty)
        {
            GenerateTraffic(PRTL);

            GameObject[] lights1 = new GameObject[4];
            GameObject[] lights2 = new GameObject[4];
            prefabOfLight.GetComponent<Light>().range = 5;
            //light1.GetComponent<Animator>().runtimeAnimatorController = null;
            for (int i = 0; i < TL.Length; i++)
            {

                lights1[i] = Instantiate(prefabOfLight, TL[i].transform, false);
                //lights1[i].GetComponent<Animator>().runtimeAnimatorController = controllerOfLight;
                lights2[i] = Instantiate(prefabOfLight, TL[i].transform, false);
                //lights2[i].GetComponent<Animator>().runtimeAnimatorController = controllerOfLight;
            }

            switch (trafficLight)//Тута сам свет наверн
            {
                case TrafficLight.off:
                    for (int i = 0; i < TL.Length; i++)
                    {
                        lights1[i].transform.localPosition = new Vector3(0, 20, -4);
                        lights2[i].transform.localPosition = new Vector3(0, 20, 4);
                    }
                    break;
                case TrafficLight.red:
                    for (int i = 0; i < TL.Length; i++)
                    {
                        lights1[i].GetComponent<Animator>().Play("ON");
                        lights2[i].GetComponent<Animator>().Play("ON");
                        lights1[i].GetComponent<Light>().color = Color.red;
                        lights1[i].transform.localPosition = new Vector3(0, 22, -4);
                        lights2[i].GetComponent<Light>().color = Color.red;
                        lights2[i].transform.localPosition = new Vector3(0, 22, 4);
                    }
                    break;
                case TrafficLight.green:
                    for (int i = 0; i < TL.Length; i++)
                    {
                        lights1[i].GetComponent<Animator>().Play("ON");
                        lights2[i].GetComponent<Animator>().Play("ON");
                        lights1[i].GetComponent<Light>().color = Color.green;
                        lights1[i].transform.localPosition = new Vector3(0, 18, -4);
                        lights2[i].GetComponent<Light>().color = Color.green;
                        lights2[i].transform.localPosition = new Vector3(0, 18, 4);
                    }
                    break;
            }
        }
    }
    public void OnAwake()
    {
        GameObject.Find("[SETUP]").AddComponent<Instantiate>();
    }
}

