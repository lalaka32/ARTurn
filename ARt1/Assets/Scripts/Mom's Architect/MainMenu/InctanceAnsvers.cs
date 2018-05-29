using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InctanceAnsvers : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ToolBox.Get<UIManager>().SetAnsversFromTest();
        ToolBox.Get<CameraManager>().SetCamGO(false);
	}
}
