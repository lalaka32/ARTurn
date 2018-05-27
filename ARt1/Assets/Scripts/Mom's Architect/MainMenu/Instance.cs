using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ToolBox.Get<CameraManager>().SetCamGO();
        ToolBox.Get<UIManager>().SetMainMenu();

    }
	
}
