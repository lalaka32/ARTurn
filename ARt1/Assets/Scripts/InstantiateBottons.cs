using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBottons : MonoBehaviour {
    [SerializeField]
    GameObject[] bottons = new GameObject[3];

    public  void CreateBottons(int size)
    {
        for (int i = 0; i < size; i++)
        {
            bottons[i].SetActive(true);
        }
    }
    public  void Clear()
    {
        foreach (GameObject item in bottons)
        {
            item.SetActive(false);
        }
    }
}