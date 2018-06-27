using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public FadeScenes fadeScenes;
    public BallController ball;
    public WaterTank tank;
    public ForestController forest;
    public GameObject cloudPrefab;
    public int maxedOutTrees;
    private float proc = 10;
    private float procCount = 0;
    private float percepitationRate = 0.5f;

	void Start () {
		
	}
	
	void Update () {
        this.procCount += Time.deltaTime;
        if(this.procCount >= proc)
        {
            this.procCount = 0f;
            this.proc = Mathf.Clamp(this.proc + 5, 10, 30);
            if (Random.Range(0f, 1f) <= this.percepitationRate)
            {
                Instantiate(cloudPrefab);
            }
        }

		if(!this.ball.isAlive)
        {
            this.fadeScenes.LoadSceneAsync("GameOver");
            return;
        }

        if(!this.forest.CanTreesLevelUp())
        {
            this.fadeScenes.LoadSceneAsync("Win");
            return;
        }

        maxedOutTrees = this.forest.MaxedOutTrees();

    }
}
