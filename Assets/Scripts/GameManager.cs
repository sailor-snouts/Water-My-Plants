using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public FadeScenes fadeScenes;
    public BallController ball;
    public WaterTank tank;
    public ForestController forest;
    public SpriteRenderer grass;
    public ScoreBoard scoreBoard;
    private AudioClip[] clips;
    private float percepitationRate = 0.8f;
    private AudioSource[] audio;

	void Start ()
    {
        this.audio = this.gameObject.GetComponentsInChildren<AudioSource>();
	}
	
	void Update () {        
        // show level progress
        float completion = this.forest.GetCompletion(); ;
        
        // scoreboard
        scoreBoard.SetScore(completion*100f);

        // grass color
        Color tmp = this.grass.color;
        tmp.a = completion;
        this.grass.color = tmp;

        // sounds
        int i = 0;
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
