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
            Invoke("DestroyAnimators", 0.3f);
        }
    }
    void DestroyAnimators()
    {
        Destroy(gameObject.transform.parent.GetComponent<Animator>());
        Destroy(othercar.transform.parent.GetComponent<Animator>());
    }
}
