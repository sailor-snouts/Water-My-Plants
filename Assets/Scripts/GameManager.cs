using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public FadeScenes fadeScenes;
    public BallController ball;
    public WaterTank tank;
    public ScoreBoard scoreBoard;
    public ForestController forest;
    public VoiceController voices;
    public GameObject ground;
    private int groundCount;
    private SpriteRenderer[] spriteR;
    private float percepitationRate = 0.8f;
    

	void Start ()
    {
        this.spriteR = this.ground.GetComponentsInChildren<SpriteRenderer>();
        this.groundCount = this.spriteR.Length;
	}
	
	void Update () {        
        // show level progress
        float completion = this.forest.GetCompletion(); ;
        float prettyCompletion = Mathf.Floor(Mathf.Clamp(completion * 120f, 0f, 100f));

        // scoreboard
        scoreBoard.SetScore(prettyCompletion);

        float delta = 1f / (float) this.groundCount;
        int i = 0;
        for (i = 0; i < this.groundCount; i++)
        {
            if(completion+delta > i*delta)
            {
                Color tmp = this.spriteR[i].color;
                tmp.a = 1f;
                this.spriteR[i].color = tmp;
            }
            else
            {
                Color tmp = this.spriteR[i].color;
                tmp.a = 0f;
                this.spriteR[i].color = tmp;
            }
        }

        this.voices.HandleVoices(completion);

        // lose
        if (!this.ball.isAlive)
        {
            this.fadeScenes.LoadSceneAsync("GameOver");
            return;
        }

        // win
        if(prettyCompletion == 100f)
        {
            this.fadeScenes.LoadSceneAsync("Win");
            return;
        }
    }
}
