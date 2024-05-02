using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningSceneLoader : MonoBehaviour
{
    public float sceneLoadDelay;
    public string sceneName;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(1);
    }
}
