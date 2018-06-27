using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    public float ballInitialVelocity = 600f;
    public Transform paddle;
    public GameObject tank;
    public bool isAlive = true;

    private WaterTank tankScript;
    private Rigidbody2D rb;
    private bool ballInPlay;
    private float stuckPositionY = 0.44f;
    private ScoreBoard scoreBoard;

    [SerializeField] int pointsPerHit = 12;

    void Awake () {
        this.rb = gameObject.GetComponent<Rigidbody2D>();
        this.tankScript = tank.GetComponent<WaterTank>();
        this.scoreBoard = FindObjectOfType<ScoreBoard>();
    }
	
	void Update ()
    {
        // game started do nothing
        if (ballInPlay)
        {
            return;
        } 

        // keep locked to paddle
        Vector3 parent = this.paddle.position;
        parent.y += this.stuckPositionY;
        transform.position = parent;

        // set free
        if (Input.GetButtonDown("Fire1"))
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

        // adjust theta based on where it hit the paddle
        if (col.gameObject.tag == "Player")
        {
            this.rb.velocity = Vector3.zero;
            Vector3 point = col.contacts[0].point;
            BoxCollider2D box = col.gameObject.gameObject.GetComponent<BoxCollider2D>();
            float thetaMultiplier = (point.x - box.bounds.min.x) / box.bounds.size.x;
            thetaMultiplier = Mathf.Clamp(thetaMultiplier, 0.15f, 0.85f);
            Vector3 dir = Quaternion.AngleAxis(thetaMultiplier * 180f, Vector3.back) * Vector3.left;
            rb.AddForce(dir * this.ballInitialVelocity);
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
            this.tankScript.UseWater(20);
        }

        // Tree
        if (col.gameObject.tag == "Plant")
        {
            TreeController tree = col.gameObject.GetComponent<TreeController>();
            if(this.tankScript.HasWater(tree.GetLevelCost()) && tree.CanLevelUp())
            {
                this.tankScript.UseWater(tree.GetLevelCost());
                tree.LevelUp();
                scoreBoard.ScoreHit(pointsPerHit);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Water
        if (col.gameObject.tag == "Water")
        {
            WaterSource source = col.gameObject.GetComponent<WaterSource>();
            int empty = this.tankScript.GetWaterCapacity() - this.tankScript.GetWaterAmount();
            this.tankScript.AddWater(source.GetWater(empty));
        }
    }
}
