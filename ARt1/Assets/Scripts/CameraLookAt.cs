using UnityEngine;
using System.Collections;

public class CameraLookAt : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.forward* 30 *Time.deltaTime);
    }
}