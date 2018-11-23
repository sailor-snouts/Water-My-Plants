using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FadeScenes fadeScenes;
    public BallController ball;
    public WaterTank tank;
    public ForestController forest;
    public VoiceController voices;
    public GameObject ground;
    private ProgressBar progressBar;
    private int groundCount;
    private SpriteRenderer[] spriteR;
    private float percepitationRate = 0.8f;
    private float completion = 0f;
    

	void Start ()
    {
        this.spriteR = this.ground.GetComponentsInChildren<SpriteRenderer>();
        this.groundCount = this.spriteR.Length;
        this.progressBar = FindObjectOfType<ProgressBar>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
	
	void Update ()
    {
        if (this.forest.GetCompletion() != this.completion)
        {
            this.completion = this.forest.GetCompletion();

            this.progressBar.SetGoal(completion);

            float delta = 1f / (float)this.groundCount;
            int i = 0;
            for (i = 0; i < this.groundCount; i++)
            {
                if (completion + delta > i * delta)
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
        }

        // lose
        if (!this.ball.isAlive)
        {
            this.fadeScenes.LoadScene("GameOver");
            return;
        }

        // win
        if(completion >= 0.99f)
        {
            this.fadeScenes.LoadScene("Win");
            return;
        }
    }
}
