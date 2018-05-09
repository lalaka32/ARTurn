using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isstopactivating : MonoBehaviour {

    public void Isstop()
    {
        gameObject.transform.parent.GetComponent<Car>().isstop = true;
        Debug.Log("isstop = true;");
    }
}
