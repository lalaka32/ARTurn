using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour {

    public GameObject Car;
    // Use this for initialization

    private GameObject[] masCars = new GameObject[4];

    void Start() {

        InstantiateCars();
        SetPositionsAndAngles();
        
    }
    


    void InstantiateCars()
    {
        for (int i = 0; i < 4; i++)
        {
            masCars[i] = Instantiate(Car, transform, false);
            masCars[i].transform.localScale = new Vector3(0.2000001f, 0.2f, 0.2000001f);
        }
    }

    void SetPositionsAndAngles()
    {
        masCars[0].transform.localPosition = new Vector3(0, 1.6f, 15);

        masCars[1].transform.localPosition = new Vector3(15, 1.6f, 0);

        masCars[1].transform.localEulerAngles = new Vector3(0, -90, 0);

        masCars[2].transform.localPosition = new Vector3(0.178f, 0.05449999f, -0.712f);//p4
        masCars[2].transform.localEulerAngles = new Vector3(0, 180, 0);

        masCars[3].transform.localPosition = new Vector3(-0.6815f, 0.0545f, -0.163f);//p1
        masCars[3].transform.localEulerAngles = new Vector3(0, 90, 0);
    }
}
