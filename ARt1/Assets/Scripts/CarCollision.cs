using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour {

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        
        if ( collision.gameObject.transform.parent.tag == "BotCar")
        {
            Debug.Log("Collision!!!");
            Destroy(gameObject.transform.parent.GetComponent<Animator>());
            Destroy(collision.gameObject.transform.parent.GetComponent<Animator>());
            
        }



        Debug.Log("bye");
    }
}
