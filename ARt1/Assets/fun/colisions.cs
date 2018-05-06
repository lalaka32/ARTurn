using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisions : MonoBehaviour {

    public Animator tAnim;
    // Use this for initialization
    void Start () {
       gameObject.AddComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>()==collision.collider)
        {
            Destroy(gameObject.GetComponent<Animator>());
            
        }
        


        Debug.Log("bye");
    }
}
