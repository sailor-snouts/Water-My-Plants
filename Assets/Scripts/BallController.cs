using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    public float ballInitialVelocity = 600f;
    public Transform paddle;
    public GameObject tank;
    public bool isAlive = true;

    private AudioSource audio;
    public AudioClip splash;
    public AudioClip tree;
    private WaterTank tankScript;
    private Rigidbody2D rb;
    private bool ballInPlay;
    private float stuckPositionY = 0.75f;
    private Queue<Vector2> hits = new Queue<Vector2>();
    private int hitRepeatCount = 0;
    private bool adjustVelocity;

    void Awake () {
        this.rb = gameObject.GetComponent<Rigidbody2D>();
        this.tankScript = tank.GetComponent<WaterTank>();
        this.audio = gameObject.GetComponent<AudioSource>();
    }
	
	void FixedUpdate ()
    {
        // game started do nothing
        if (ballInPlay)
        {
            if(this.adjustVelocity)
            {
                this.rb.velocity = Quaternion.AngleAxis(Random.Range(-5f, 5f), Vector3.forward) * this.rb.velocity;

                this.adjustVelocity = false;
            }

            return;
        } 

        // keep locked to paddle
        Vector3 parent = this.paddle.position;
        parent.y += this.stuckPositionY;
        transform.position = parent;
    }

    private void Update()
    {
        if (!ballInPlay && Input.GetButtonDown("Fire1") || !ballInPlay && Input.GetKeyDown(KeyCode.Space))
        {
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(0, ballInitialVelocity, 0));
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!ballInPlay)
        {
            return;
        }

        this.hits.Enqueue(this.transform.position);

        while (this.hits.Count > 12)
        {
            this.hits.Dequeue();
        }

        this.hitRepeatCount = 0;
        foreach (Vector2 hit in this.hits)
        {
            if (Vector2.Distance(this.transform.position, hit) < 0.1f)
            {
                this.hitRepeatCount++;
            }
        }
        
        if (this.hitRepeatCount >= 3)
        {
            this.adjustVelocity = true;
        }

        // adjust theta based on where it hit the paddle
        if (col.gameObject.tag == "Player")
        {
            this.rb.velocity = Vector3.zero;
            Vector2 dir = this.gameObject.transform.position - col.gameObject.transform.position;
            rb.AddForce(dir.normalized * this.ballInitialVelocity);

            return;
        }

        // deadzone, reset ball
        if (col.gameObject.tag == "Deadzone")
        {
            this.rb.velocity = Vector3.zero;
            this.ballInPlay = false;
            if (!this.tankScript.HasWater())
            {
                this.isAlive = false;
                return;
            }
            this.tankScript.UseWater(25);

            return;
        }

        // Plant
        if (col.gameObject.tag == "Plant")
        {
            TreeController tree = col.gameObject.GetComponent<TreeController>();
            if(this.tankScript.HasWater(tree.GetLevelCost()) && tree.CanLevelUp())
            {
                this.tankScript.UseWater(tree.GetLevelCost());
                tree.LevelUp();
                this.audio.clip = this.tree;
                this.audio.Play();

                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Tree
        if (col.gameObject.tag == "Flower")
        {
            TreeController tree = col.gameObject.GetComponent<TreeController>();
            if (this.tankScript.HasWater(tree.GetLevelCost()) && tree.CanLevelUp())
            {
                this.tankScript.UseWater(tree.GetLevelCost());
                tree.LevelUp();
            }

        }

        // Water
        if (col.gameObject.tag == "Water")
        {
            WaterSource source = col.gameObject.GetComponent<WaterSource>();
            int empty = this.tankScript.GetWaterCapacity() - this.tankScript.GetWaterAmount();
            this.tankScript.AddWater(source.GetWater(empty));
            if(empty > 0)
            {
                this.audio.clip = this.splash;
                this.audio.Play();
            }
        }
    }
}
