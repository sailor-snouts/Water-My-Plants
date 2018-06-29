using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    /**
     * 
     * Set render mode to world space
     * set scale based on pixel to unit ration 100px/unit means a 0.01 scale
     * set the camera
     *
     */
     
    Text scoreText;

	// Use this for initialization
	void Awake () {
        scoreText = gameObject.GetComponent <Text>();
        scoreText.text = "0%";
	}
	
	public void SetScore(float completion) {
        scoreText.text = completion.ToString() + "%";
    }
}
