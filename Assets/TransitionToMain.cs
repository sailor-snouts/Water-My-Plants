using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionToMain : MonoBehaviour {

    FadeScenes fadeScenes;

    void Start() {
       
        fadeScenes = GetComponent<FadeScenes>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKey) {
            fadeScenes.LoadSceneAsync("Main");
        }
    }
}
