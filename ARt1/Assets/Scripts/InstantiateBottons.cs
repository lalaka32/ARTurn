using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBottons : MonoBehaviour {
    public GameObject[] bottons = new GameObject[3];
    public void CreateBottons(int size)
    {
        for (int i = 0; i < size; i++)
        {
            bottons[i].SetActive(true);
        }
    }
    public void Clear()
    {
        for (int i = 0; i < bottons.Length; i++)
        {
            bottons[i].SetActive(false);
        }
    }
}