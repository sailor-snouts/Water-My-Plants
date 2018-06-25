using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public FadeScenes fadeScenes;
    public BallController ball;
    public WaterTank tank;
    public TreeController[] trees;

	void Start () {
		
	}
	
	void Update () {
		if(!this.ball.isAlive)
        {
            this.fadeScenes.LoadSceneAsync("GameOver");
            return;
        }

        foreach(TreeController tree in this.trees)
        {
            if(tree.CanLevelUp())
            {
                return;
            }
        }
        this.fadeScenes.LoadSceneAsync("Win");
        return;
    }
}
