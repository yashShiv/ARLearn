using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelLoaderScript : MonoBehaviour
{
    //bool sceneLoaded = false;
    static PlaceObjectOnOrigin debugPlacer;
    static PlaceObjectOnPlane placer;
    static GameObject modelToPlace;
    
    [HideInInspector]
    public int lastSceneIndex;

    [Tooltip("(DEBUGGING)Turn it on if only normal camera is enabled in ARScreen scene.")]
    public bool debug = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnload;
        Debug.Log("Wake Up");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //sceneLoaded = true;
        if (scene.name == "ARScreen")
        {
            GameObject.Find("EventSystem").GetComponent<InfoPanelManager>().displayText = modelToPlace.GetComponent<ModelInfo>().getModelInfo();
            if (debug)
            {
                debugPlacer = GameObject.FindGameObjectWithTag("DebugCamera").GetComponent<PlaceObjectOnOrigin>();
                debugPlacer.SetObject(modelToPlace);
            }
            else
            {
                placer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlaceObjectOnPlane>();
                placer.SetObject(modelToPlace);
            }
        }
        Debug.Log("Loaded " + scene.name);
    }

    private void OnSceneUnload(Scene scene)
    {
        //sceneLoaded = false;
        lastSceneIndex = scene.buildIndex;
        Debug.Log("Unloaded " + scene.name);
    }

    public void LoadModel(GameObject model)
    {
        GameObject.Find("SceneLoadManager").GetComponent<SceneLoaderScript>().displayScene(4);
        modelToPlace = model;
        Debug.Log("Got Model: " + modelToPlace.name);
    }
}
