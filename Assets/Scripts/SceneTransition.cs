using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {

    [SerializeField] public string transitionMessage = "Press any key to continue";
    [SerializeField] public string sceneTarget = "Title";

    Text transitionText;

    void Start () {
        transitionText = GetComponent<Text>();
        transitionText.text = transitionMessage;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey) {
            SceneManager.LoadScene(sceneTarget);
        }
    }
}
