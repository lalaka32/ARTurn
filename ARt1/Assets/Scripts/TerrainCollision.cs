using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>().useGravity == true)
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            StartCoroutine(ToTheSky(other));
        }
    }
    IEnumerator ToTheSky(Collider other)
    {
        while (true)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce((other.transform.forward + Vector3.up) * 10);
                yield return new WaitForFixedUpdate();
        }
    }
}
