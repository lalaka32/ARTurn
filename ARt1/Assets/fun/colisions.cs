using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisions : MonoBehaviour {

    public Animator tAnim;
    // Use this for initialization
    void Start () {
       gameObject.AddComponent<Animator>();
    }
	
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>()==collision.collider)
        {
            Destroy(gameObject.GetComponent<Animator>());
            
        }
    }
}
