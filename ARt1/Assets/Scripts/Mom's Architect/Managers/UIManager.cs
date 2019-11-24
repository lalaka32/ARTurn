
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
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

    Transform mainPanel;
    Transform registerPanel;
    Transform authPanel;
    Transform settingsPanel;

    public void SetMainMenu()
	{
		MainMenu = Instantiate(mainMenuPrefab, GameObject.Find("UI").transform, false);
        if (ToolBox.Get<DataGameSession>().Token != null || ToolBox.Get<DataGameSession>().Token != "")
        {
            mainPanel = MainMenu.transform.Find("MainPanel");
            mainPanel.Find("Authorisation").gameObject.SetActive(false);
            mainPanel.Find("Logout").gameObject.SetActive(true);
        }
        else
        {
            mainPanel.Find("Authorisation").gameObject.SetActive(true);
        }
        MainMenu.GetComponent<Canvas>().worldCamera = ToolBox.Get<CameraManager>().MainCamera.GetComponent<Camera>();

        SetMainMenuButtonsEvents();
	}

    void SetMainMenuButtonsEvents()
	{
        mainPanel = MainMenu.transform.Find("MainPanel");
        registerPanel = MainMenu.transform.Find("RegisterPanel");
        authPanel = MainMenu.transform.Find("AuthPanel");
        settingsPanel = MainMenu.transform.Find("SettingsPanel");
        mainPanel.Find("Play").GetComponent<Button>().onClick.AddListener(delegate
		{
			SceneManager.LoadScene(2);
			ToolBox.Get<SettingsPlayer>().ARCamera = MainMenu.transform.Find("SettingsPanel").transform.Find("ToggleOfTypeCamera").GetComponent<Toggle>().isOn;
		});

		mainPanel.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(delegate
		{
			Application.Quit();
		});
		mainPanel.Find("Options").GetComponent<Button>().onClick.AddListener(delegate
		{
            mainPanel.gameObject.SetActive(false);
			settingsPanel.gameObject.SetActive(true);
		});

        mainPanel.Find("Authorisation").GetComponent<Button>().onClick.AddListener(delegate
        {
            mainPanel.gameObject.SetActive(false);
            authPanel.gameObject.SetActive(true);
        });

        mainPanel.transform.Find("Logout").GetComponent<Button>().onClick.AddListener(delegate
        {
            ToolBox.Get<DataGameSession>().Token = null;
            Debug.Log("now token is null");
            mainPanel.Find("Logout").gameObject.SetActive(false);
            mainPanel.Find("Authorisation").gameObject.SetActive(true);
        });

        settingsPanel.transform.Find("Home").GetComponent<Button>().onClick.AddListener(delegate
		{
			settingsPanel.gameObject.SetActive(false);
            mainPanel.gameObject.SetActive(true);
		});

        authPanel.transform.Find("Home").GetComponent<Button>().onClick.AddListener(delegate
        {
            authPanel.gameObject.SetActive(false);
            mainPanel.gameObject.SetActive(true);
        });
        authPanel.transform.Find("Register").GetComponent<Button>().onClick.AddListener(delegate
        {
            authPanel.gameObject.SetActive(false);
            registerPanel.gameObject.SetActive(true);
        });

        registerPanel.transform.Find("Home").GetComponent<Button>().onClick.AddListener(delegate
        {
            registerPanel.gameObject.SetActive(false);
            mainPanel.gameObject.SetActive(true);
        });

        registerPanel.transform.Find("Register").GetComponent<Button>().onClick.AddListener(delegate
        {
            InputField login = registerPanel.Find("InputLogin").GetComponent<InputField>();
            InputField password = registerPanel.Find("InputPassword").GetComponent<InputField>();
            InputField confirmedPassword = registerPanel.Find("ConfirmPassword").GetComponent<InputField>();
            ToolBox.Get<AuthorisationManager>().Register(login, password, confirmedPassword);
        });

        authPanel.transform.Find("Login").GetComponent<Button>().onClick.AddListener(delegate
        {
            InputField login = authPanel.Find("InputLogin").GetComponent<InputField>();
            InputField password = authPanel.Find("InputPassword").GetComponent<InputField>();
            ToolBox.Get<AuthorisationManager>().Login(login, password);
        });
    }

    public void HideAuthorisation()
    {
        mainPanel.Find("Authorisation").gameObject.SetActive(false);
        mainPanel.Find("Logout").gameObject.SetActive(true);
        authPanel.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);

    }

    public void SetAnsverButtons()
	{
		buttons = new GameObject[3];
		Canvas = Instantiate(canvasPrefab, GameObject.Find("UI").transform, false);
		Canvas.transform.Find("Home").GetComponent<Button>().onClick.AddListener(delegate
		{
			ShowMainMenu();
		});
		for (int i = 1; i < 4; i++)
		{
			buttons[i - 1] = Canvas.transform.Find(i.ToString()).gameObject;
		}
		#region IGiveUp(Buttons)
		Canvas.transform.Find(string.Format("{0}", 1.ToString().Trim())).GetComponent<Button>().onClick.AddListener(
			delegate
			{
				ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(0);
				GameObject.Find("[SETUP]").GetComponent<Instantiate>().timer.Stop();
			});
		Canvas.transform.Find(string.Format("{0}", 2.ToString().Trim())).GetComponent<Button>().onClick.AddListener(
			delegate
			{
				ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(1);
				GameObject.Find("[SETUP]").GetComponent<Instantiate>().timer.Stop();
			});
		Canvas.transform.Find(string.Format("{0}", 3.ToString().Trim())).GetComponent<Button>().onClick.AddListener(
			delegate
			{
				ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(2);
				GameObject.Find("[SETUP]").GetComponent<Instantiate>().timer.Stop();
			});
		#endregion
		Canvas.gameObject.transform.Find("Win").Find("Next").GetComponent<Button>().onClick.AddListener(delegate
		{
			GameObject.Find("[SETUP]").GetComponent<Instantiate>().Restart = true;
		});
		Canvas.gameObject.transform.Find("Lose").Find("Next").GetComponent<Button>().onClick.AddListener(delegate
		{
			if (ToolBox.Get<ProcessingAnsvers>().mistakesese.Count < 1)
			{
				Debug.Log(ToolBox.Get<ProcessingAnsvers>().mistakesese.Count + "ToolBox.Get<ProcessingAnsvers>().mistakesese.Count");
				ToolBox.Get<ProcessingAnsvers>().mistakesese.Add(ToolBox.Get<ProcessingAnsvers>().lvlSituat.Count);
				GameObject.Find("[SETUP]").GetComponent<Instantiate>().Restart = true;
			}
			else
			{
				ToolBox.Get<ProcessingAnsvers>().mistakesese.Add(ToolBox.Get<ProcessingAnsvers>().lvlSituat.Count);
                ToolBox.Get<SendingInfoManager>().SendTest();
                ToolBox.Get<UIManager>().ShowResults();
			}
		});
	}

	public void ShowLoseMenu()
	{
		Canvas.gameObject.transform.Find("Lose").gameObject.SetActive(true);
		Canvas.gameObject.transform.Find("Lose").Find("Next").GetComponent<Button>().onClick.AddListener(delegate
		{
			Canvas.gameObject.transform.Find("Lose").gameObject.SetActive(false);
		});
	}
	public void ShowWinMenu()
	{
		Canvas.gameObject.transform.Find("Win").gameObject.SetActive(true);
		Canvas.gameObject.transform.Find("Win").Find("Next").GetComponent<Button>().onClick.AddListener(delegate
		{
			Canvas.gameObject.transform.Find("Win").gameObject.SetActive(false);
		});
	}
	public void SetUIForRevision()
	{
		buttons = new GameObject[3];
		Canvas = Instantiate(canvasPrefab, GameObject.Find("UI").transform, false);
		for (int i = 1; i < 4; i++)
		{
			buttons[i - 1] = Canvas.transform.Find(i.ToString()).gameObject;
		}

		#region SetButtons
		Canvas.transform.Find(string.Format("{0}", 1.ToString().Trim())).GetComponent<Button>().onClick.AddListener(delegate
		 {
			 GameObject.Find("[SETUP]").GetComponent<InstantceParticular>().timer.Stop();
			 ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(0);
		 });
		Canvas.transform.Find(2.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			GameObject.Find("[SETUP]").GetComponent<InstantceParticular>().timer.Stop();
			ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(1);
		});
		Canvas.transform.Find(3.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			GameObject.Find("[SETUP]").GetComponent<InstantceParticular>().timer.Stop();
			ToolBox.Get<CrossManager>().Cross.GetComponent<LOGIC_V001>().MakeLogicOnAns(2);
		});
		#endregion
		Canvas.gameObject.transform.Find("Win").Find("Next").GetComponent<Button>().onClick.AddListener(delegate
		{
			//new Color(255, 104, 0)
			ToolBox.Get<ProcessingAnsvers>().mistakesese.Remove(ToolBox.Get<SettingsPlayer>().numberOfRevisionQuestion + 1);
			ShowResults();
		});
		Canvas.gameObject.transform.Find("Lose").Find("Next").GetComponent<Button>().onClick.AddListener(delegate
		{
			ShowRevision();
		});
	}
	public void SetAnsversFromTest()
	{
		buttons = new GameObject[10];

		ConclusionMenu = Instantiate(conclusionMenuPrefab, GameObject.Find("UI").transform, false);
		for (int i = 1; i < 11; i++)
		{
			buttons[i - 1] = ConclusionMenu.transform.Find(i.ToString()).gameObject;
		}
		for (int i = 0; i < ToolBox.Get<ProcessingAnsvers>().mistakesese.Count; i++)
		{
			//Debug.Log(ToolBox.Get<ProcessingAnsvers>().mistakesese[i].ToString());
			ConclusionMenu.transform.Find(ToolBox.Get<ProcessingAnsvers>().mistakesese[i].ToString()).GetComponent<Image>().color = Color.red;
		}
		ConclusionMenu.transform.Find("Text").GetComponent<Text>().text = (ToolBox.Get<ProcessingAnsvers>().lvlSituat.Count - ToolBox.Get<ProcessingAnsvers>().mistakesese.Count) + " out of " + ToolBox.Get<ProcessingAnsvers>().lvlSituat.Count + "" + ConclusionMenu.transform.Find("Text").GetComponent<Text>().text;
		ConclusionMenu.transform.Find("Home").GetComponent<Button>().onClick.AddListener(delegate
		{
			ShowMainMenu();
		});
		ConclusionMenu.transform.Find("Restart").GetComponent<Button>().onClick.AddListener(delegate
		{
			ShowTest();
		});
		#region ButtonsReview
		ConclusionMenu.transform.Find(1.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(1 - 1);
			ShowRevision();
		});
		ConclusionMenu.transform.Find(2.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(2 - 1);
			ShowRevision();
		});
		ConclusionMenu.transform.Find(3.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(3 - 1);
			ShowRevision();
		});
		ConclusionMenu.transform.Find(4.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(4 - 1);
			ShowRevision();
		});
		ConclusionMenu.transform.Find(5.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(5 - 1);
			ShowRevision();
		});
		ConclusionMenu.transform.Find(6.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(6 - 1);
			ShowRevision();
		});
		ConclusionMenu.transform.Find(7.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(7 - 1);
			ShowRevision();
		});
		ConclusionMenu.transform.Find(8.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(8 - 1);
			ShowRevision();
		});
		ConclusionMenu.transform.Find(9.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(9 - 1);
			ShowRevision();
		});
		ConclusionMenu.transform.Find(10.ToString().Trim()).GetComponent<Button>().onClick.AddListener(delegate
		{
			ToolBox.Get<SettingsPlayer>().SetNumber(10 - 1);
			ShowRevision();
		});
		#endregion//Срочно поменять
	}
	public void ShowMainMenu()
	{
		SceneManager.LoadScene(1);
	}
	public void ShowTest()
	{
		SceneManager.LoadScene(2);
	}
	public void ShowResults()
	{
		SceneManager.LoadScene(3);
	}
	public void ShowRevision()
	{
		SceneManager.LoadScene(4);
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
	public void ClearPrefab()
	{
		if (Canvas != null)
		{
			Destroy(Canvas);
		}

	}
	public void SetTimerValue(float time)
	{
		if (Canvas != null)
		{
			Canvas.transform.Find("Timer").GetComponentInChildren<Text>().text = string.Format("{0:f2}", time);
		}
	}
}