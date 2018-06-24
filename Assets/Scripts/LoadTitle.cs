using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTitle : MonoBehaviour {

	void Start () {
        Invoke("LoadTitleScreen", 3f);
	}
	
	void LoadTitleScreen() {
        SceneManager.LoadScene(1);
    }
}
