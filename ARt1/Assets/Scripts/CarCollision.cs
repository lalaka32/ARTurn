using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour {

    GameObject othercar;
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        
        if ( collision.gameObject.transform.parent.transform.parent.tag == "BotCar"| collision.gameObject.transform.parent.transform.parent.tag =="VIP")
        {
            othercar = collision.gameObject;
            DestroyAnimators();
        }
    }
    void DestroyAnimators()
    {
        if (gameObject.transform.parent.GetComponent<Animator>()!=null)
        {
            Destroy(gameObject.transform.parent.GetComponent<Animator>());
        }
        if (othercar.transform.parent.GetComponent<Animator>()!=null)
        {
            Destroy(othercar.transform.parent.GetComponent<Animator>());
        }

    }
}
