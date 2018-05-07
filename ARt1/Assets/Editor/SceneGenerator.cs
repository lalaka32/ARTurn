using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


[InitializeOnLoad]//Помогает чё-то делать при созданиии сцен
public class SceneGenerator  {

    static SceneGenerator()
    {
        EditorSceneManager.newSceneCreated += SceneCreating;
    }
	public static void SceneCreating(Scene scene, NewSceneSetup setup,NewSceneMode mode)
    {
        var camGO = Camera.main.transform;
        var lightGO = GameObject.Find("Directional Light").transform;
        var setupFoulder = new GameObject("[SETUP]").transform;
        var lights = new GameObject("Lights").transform;
        lights.parent = setupFoulder;
        lightGO.parent = lights;
        var cam = new GameObject("Cameras").transform;
        cam.parent = setupFoulder;
        camGO.parent = cam;
        var world = new GameObject("[World]").transform;
        new GameObject("Static").transform.parent = world;
        new GameObject("Dynamic").transform.parent = world;
        new GameObject("UI");
        Debug.Log("NEW SCENE");
    }
}
