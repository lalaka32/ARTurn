using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour {

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(ToTheSky(collision));
    }
    IEnumerator ToTheSky(UnityEngine.Collision collision)
    {
        while (true)
        {
            collision.gameObject.transform.parent.parent.Translate(Vector3.up * Time.deltaTime * 4);
            yield return new WaitForEndOfFrame();
        }
    } 
}
