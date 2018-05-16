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
        
        Debug.Log("isstop = true;");
    }
    //public void IsEnd()
    //{
    //    StartCoroutine(ToTheHell());
    //}
    IEnumerator ToTheHell()
    {
        Vector3 endposition = transform.GetChild(0).position + transform.GetChild(0).TransformVector(new Vector3(0, 0, 50));
        while (transform.GetChild(0).position.y < 1)
        {
            transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, endposition, Time.deltaTime * 0.5f);
            yield return new WaitForFixedUpdate();
        }
    }
}
