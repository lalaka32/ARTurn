using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOGIK_V001 : MonoBehaviour {

    string question;
    Dictionary<byte,GameObject> bigDick;
    public GameObject[] masCars;

    // Use this for initialization
    void Start () {
        masCars = GetComponent<Instantiate>().masCars;
        // METOD V INSTANCIATE
        
    }
	void InitAnsUser(byte playerPriority)
    {
        bigDick.Add(playerPriority, masCars[0]);
    }
	// Update is called once per frame
	void Update () {
		
	}

}
