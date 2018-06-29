using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public FadeScenes fadeScenes;
    public BallController ball;
    public WaterTank tank;
    public ScoreBoard scoreBoard;
    public ForestController forest;
    public GameObject ground;
    private int groundCount;
    private SpriteRenderer[] spriteR;
    private AudioClip[] clips;
    private float percepitationRate = 0.8f;
    private AudioSource[] audio;

	void Start ()
    {
        this.audio = this.gameObject.GetComponentsInChildren<AudioSource>();
        this.spriteR = this.ground.GetComponentsInChildren<SpriteRenderer>();
        this.groundCount = this.spriteR.Length;
	}
	
	void Update () {        
        // show level progress
        float completion = this.forest.GetCompletion(); ;
        
        // scoreboard
        scoreBoard.SetScore(completion*100f);

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


        // sounds
        float clip_count = this.audio.Length;
        foreach(AudioSource clip in this.audio)
        {
            i++;
            float clip_rate = i / clip_count;
            if (completion >= clip_rate || i == 1)
            {
                clip.volume = 1f;
            }
            else
            {
                clip.volume = 0f;
            }
        }

        // lose
        if (!this.ball.isAlive)
        {
            this.fadeScenes.LoadSceneAsync("GameOver");
            return;
        }

        // win
        if(!this.forest.CanTreesLevelUp())
        {
            this.fadeScenes.LoadSceneAsync("Win");
            return;
        }
    }
}
