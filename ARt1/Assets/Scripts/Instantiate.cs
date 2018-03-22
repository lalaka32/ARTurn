using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour {
    public GameObject Car;
    // Use this for initialization
    private GameObject[] masCars = new GameObject[4];
    void Start() {
        for (int i = 0; i < 4; i++)
        {
            masCars[i] = Instantiate(Car, transform,false);
            masCars[i].transform.localScale = new Vector3(5, 7, 5);
        }
        
        masCars[0].transform.localPosition = new Vector3(0, 1.6f, 15);

        masCars[1].transform.localPosition = new Vector3(15, 1.6f, 0);
        masCars[1].transform.localEulerAngles = new Vector3(0, -90, 0);

        masCars[2].transform.localPosition = new Vector3(0, 1.6f, -15);
        masCars[2].transform.localEulerAngles = new Vector3(0, 180, 0);

        masCars[3].transform.localPosition = new Vector3(-15, 1.6f, 0);
        masCars[3].transform.localEulerAngles = new Vector3(0, 90, 0);
        
    }
    

}
