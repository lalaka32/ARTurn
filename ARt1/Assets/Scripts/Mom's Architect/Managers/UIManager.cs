
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UIManager", menuName = "Managers/UIManager")]
class UIManager : ManagerBase
{
    [SerializeField]
    GameObject canvasPrefab;
    [SerializeField]
    GameObject mainMenuPrefab;
    [SerializeField]
    GameObject conclusionMenuPrefab;


    public GameObject Canvas { get; private set; }
    public GameObject MainMenu { get; private set; }
    public GameObject ConclusionMenu { get; set; }

    GameObject[] buttons;

    public void SetMainMenu()
    {
        MainMenu = Instantiate(mainMenuPrefab, GameObject.Find("UI").transform, false);
        MainMenu.GetComponent<Canvas>().worldCamera = ToolBox.Get<CameraManager>().MainCamera.GetComponent<Camera>();
        SetMenuButtonsEvents();
    }
    void SetMenuButtonsEvents()
    {
        MainMenu.transform.Find("MainPanel").transform.Find("Play").GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene(2);
            ToolBox.Get<SettingsPlayer>().ARCamera = MainMenu.transform.Find("SettingsPanel").transform.Find("ToggleOfTypeCamera").GetComponent<Toggle>().isOn;
        });
        MainMenu.transform.Find("MainPanel").transform.Find("Exit").GetComponent<Button>().onClick.AddListener(delegate
        {
            Application.Quit();
        });
        MainMenu.transform.Find("MainPanel").transform.Find("Options").GetComponent<Button>().onClick.AddListener(delegate
        {
            MainMenu.transform.Find("MainPanel").gameObject.SetActive(false);
            MainMenu.transform.Find("SettingsPanel").gameObject.SetActive(true);
        });
        MainMenu.transform.Find("SettingsPanel").transform.Find("Home").GetComponent<Button>().onClick.AddListener(delegate
        {
            MainMenu.transform.Find("SettingsPanel").gameObject.SetActive(false);
            MainMenu.transform.Find("MainPanel").gameObject.SetActive(true);
        });
    }

    public void SetAnsverButtons()
    {
        buttons = new GameObject[3];
        Canvas = Instantiate(canvasPrefab, GameObject.Find("UI").transform, false);
        for (int i = 1; i < 4; i++)
        {
            buttons[i - 1] = Canvas.transform.Find(i.ToString()).gameObject;

            Canvas.transform.Find(i.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
            {
                GameObject.Find("[SETUP]").GetComponent<Instantiate>().timer.Stop();
                ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(Convert.ToInt32(i.ToString())-1);
            });
        }
        Canvas.gameObject.transform.Find("Restart").GetComponent<Button>().onClick.AddListener(delegate
            {
                GameObject.Find("[SETUP]").GetComponent<Instantiate>().timer.Stop();
                GameObject.Find("[SETUP]").GetComponent<Instantiate>().Restart = true;
                //SceneManager.LoadScene(1);
            });
    }
    public void SetUIForRevision()
    {
        buttons = new GameObject[3];
        Canvas = Instantiate(canvasPrefab, GameObject.Find("UI").transform, false);
        for (int i = 1; i < 4; i++)
        {
            buttons[i - 1] = Canvas.transform.Find(i.ToString()).gameObject;
            Canvas.transform.Find(i.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
            {
                GameObject.Find("[SETUP]").GetComponent<Instantiate>().timer.Stop();
                ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(i);
            });
        }
        Canvas.gameObject.transform.Find("Restart").GetComponent<Button>().onClick.AddListener(delegate
        {
            GameObject.Find("[SETUP]").GetComponent<Instantiate>().timer.Stop();
            SceneManager.LoadScene(3);
        });
    }
    public void SetAnsversFromTest()
    {
        ConclusionMenu = Instantiate(conclusionMenuPrefab, GameObject.Find("UI").transform, false);
        ConclusionMenu.transform.Find("Home").GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene(1);
        });
        ConclusionMenu.transform.Find("Restart").GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene(2);
        });
        for (int i = 1; i < 11; i++)
        {
            Debug.Log(i);

            ConclusionMenu.transform.Find(i.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
            {
                ToolBox.Get<SettingsPlayer>().SetNumber(i-1);
                Debug.Log(i);
                    //SceneManager.LoadScene(4);
            });

        }
    }

    public void ShowResults()
    {
        SceneManager.LoadScene(3);
    }

    public void CreateBottons(int size)
    {
        for (int i = 0; i < size; i++)
        {
            buttons[i].SetActive(true);
        }
    }
    public void ClearBottons()
    {
        foreach (GameObject item in buttons)
        {
            item.SetActive(false);
        }
    }
    public void SetTimerValue(float time)
    {
        Canvas.transform.Find("Text").GetComponentInChildren<Text>().text = string.Format("{0:f2}", time);
    }
}

