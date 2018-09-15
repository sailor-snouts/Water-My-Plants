using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionToMain : MonoBehaviour {

    [SerializeField]
    private string LoadingScene = "Scenes/Loading";
    [SerializeField]
    private string Scene = "Scenes/Main";
    [SerializeField]
    private float checkDelay = 5f;
    [SerializeField]
    private bool isLoaded = false;
    [SerializeField]
    private bool isLocked = false;
    AsyncOperation async;

    // Update is called once per frame
    void Update() {
        if (!this.isLocked && Input.anyKey)
        {
            this.isLocked = true;
            this.isLoaded = false;
            SceneManager.LoadScene(this.LoadingScene, LoadSceneMode.Additive);
            StartCoroutine(LoadNewScene(this.Scene));
        }
        else if (this.isLocked && this.isLoaded)
        {
            this.async.allowSceneActivation = true;
        }
    }

    IEnumerator LoadNewScene(string scene)
    {
        yield return new WaitForSeconds(this.checkDelay);

        this.async = Application.LoadLevelAsync(scene);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        this.isLoaded = true;
    }
}
