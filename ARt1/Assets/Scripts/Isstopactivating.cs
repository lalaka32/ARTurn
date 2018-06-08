using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isstopactivating : MonoBehaviour {

    public void Isstop()
    {
        gameObject.transform.parent.GetComponent<Car>().isstop = true;
        foreach (var item in gameObject.transform.GetChild(0).GetComponentsInChildren<Animator>())
        {
            item.SetBool("Isturneroff", true);
        }
        
    }

    IEnumerator ToTheHell()
    {
        var Vector = (gameObject.transform.forward*4f) + (Vector3.up*2) ;
        //Vector3 endposition = transform.GetChild(0).position + transform.GetChild(0).TransformVector(new Vector3(0, 0, 50));
        while (true)
        {
            gameObject.transform.Find("Body").GetComponent<Rigidbody>().AddForce(Vector * 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
