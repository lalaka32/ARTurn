using Assets.Scripts.Mom_s_Architect.DemoScripts;
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

    public void OnAwake()
    {
        GameObject.Find("[SETUP]").AddComponent<SetupUI>();
    }

    public void SetUI()
    {
        Canvas = Instantiate(canvasPrefab, GameObject.Find("UI").transform, false);
        for (int i = 1; i <= 3; i++)
        {
            Canvas.gameObject.transform.Find(i.ToString().Trim()).GetComponent<Button>().onClick.AddListener(
                delegate { ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(i);});
        }
        Canvas.gameObject.transform.Find("Restart").GetComponent<Button>().onClick.AddListener(
            delegate {
                GameObject.Find("[SETUP]").GetComponent<Instantiate>().Restart = true;
            });

    }
}

