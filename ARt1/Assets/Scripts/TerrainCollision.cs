using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour
{
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
        var Vector = other.gameObject.transform.forward+Vector3.up;
        while (true)
        {
            
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector * 1f);

            yield return new WaitForFixedUpdate();
        }
    }
}
