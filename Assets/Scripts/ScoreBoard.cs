using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    /**
     * NOTE: When instantiating the ScoreBoard prefab, you have to set the
     * Render Camera for the ScoreBoard canvas object to the camera in the game scene.
     *
     * I tried prefabbing the camera and linking it in the scoreboard prefab, but 
     * then the score quit displaying altogether.
     * 
     * Normally you can use the Overlay Render Mode with a canvas and not touch the cameras.
     * But in this case, Overlay makes the Canvas object huge and weirdly
     * positioned, making it difficult to position text elements on it correctly. By changing
     * the render mode to Screen Space - Camera, and then setting the camera to the one in the scene
     * it snaps over the view correctly so that you can lay out elements. The weird behavior in Overlay mode
     * is due to how sprites are scaled in relation to other objects inside Unity. In other words,
     * our use of Sprites makes it difficult to use Overlay mode, so we need to use Screen Space mode instead.
     *
     */

    int score = 0;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent <Text>();
        scoreText.text = score.ToString();
	}
	
	public void ScoreHit(int points) {
        score += points;
        scoreText.text = score.ToString();
    }
}
