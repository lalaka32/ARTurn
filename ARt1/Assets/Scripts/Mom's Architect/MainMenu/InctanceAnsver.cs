using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InctanceAnsver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ToolBox.Get<UIManager>().SetAnsversFromTest();
	}
}
