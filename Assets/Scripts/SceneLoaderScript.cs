using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderScript : MonoBehaviour
{
    static int lastSceneIndex;
    static int currentSceneIndex;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneIndex = scene.buildIndex;
    }

    void OnSceneUnload(Scene scene)
    {
        lastSceneIndex = scene.buildIndex;
    }

    public void displayScene(int sceneIndex)
    {
        SceneManager.LoadScene (sceneIndex);
    }

    public void displayLastScene()
    {
        displayScene(lastSceneIndex);
    }
}
