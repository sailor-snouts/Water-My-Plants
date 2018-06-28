using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public FadeScenes fadeScenes;
    public BallController ball;
    public WaterTank tank;
    public ForestController forest;
    public GameObject cloudPrefab;
    public SpriteRenderer grass;
    private AudioClip[] clips;
    private float proc = 8;
    private float procCount = 0;
    private float percepitationRate = 0.8f;
    private AudioSource[] audio;

	void Start ()
    {
        this.audio = this.gameObject.GetComponentsInChildren<AudioSource>();
	}
	
	void Update () {
        // spawn some clouds and let it rain
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
        
        // show level progress
        float completion = this.forest.GetCompletion(); ;
        Color tmp = this.grass.color;
        tmp.a = completion;
        this.grass.color = tmp;

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
