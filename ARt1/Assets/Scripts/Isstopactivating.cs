using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isstopactivating : MonoBehaviour {

    public void Isstop()
    {
        gameObject.transform.parent.GetComponent<Car>().isstop = true;
        Debug.Log("isstop = true;");
    }
    public void IsEnd()
    {
        StartCoroutine(ToTheHell());
    }
    IEnumerator ToTheHell()
    {
        int i = 0;
        while (i < 200)
        {
            transform.GetChild(0).Translate(Vector3.forward * Time.deltaTime * 15);
            i++;
            yield return new WaitForEndOfFrame();
        }
    }
}
