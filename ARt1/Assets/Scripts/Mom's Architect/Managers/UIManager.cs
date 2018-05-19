
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UIManager", menuName = "Managers/UIManager")]
class UIManager : ManagerBase, IAwake
{
    [SerializeField]
    GameObject canvasPrefab;
    public GameObject Canvas { get; set; }

    GameObject[] bottons = new GameObject[2];

    public void SetUI()
    {
        Canvas = Instantiate(canvasPrefab, GameObject.Find("UI").transform, false);
        for (int i = 1; i <= 3; i++)
        {
            bottons[i-1] = Canvas.transform.Find(i.ToString()).gameObject;
            Canvas.transform.Find(i.ToString().Trim()).GetComponent<Button>().onClick.AddListener(
                delegate { ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(i); });
        }
        Canvas.gameObject.transform.Find("Restart").GetComponent<Button>().onClick.AddListener(
            delegate {
                GameObject.Find("[SETUP]").GetComponent<Instantiate>().Restart = true;
            });

    }

    public void CreateBottons(int size)
    {
        for (int i = 0; i < size; i++)
        {
            bottons[i].SetActive(true);
        }
    }
    public void Clear()
    {
        foreach (GameObject item in bottons)
        {
            item.SetActive(false);
        }
    }

    public void OnAwake()
    {

    }
}

