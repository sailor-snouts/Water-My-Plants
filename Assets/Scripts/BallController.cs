using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    public float ballInitialVelocity = 600f;
    private Rigidbody2D rb;
    private bool ballInPlay;

    void Awake () {
        this.rb = gameObject.GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        // game started do nothing
        if (ballInPlay)
        {
            return;
        }

        // keep locked to paddle
        Vector3 parent = transform.parent.gameObject.transform.position;
        parent.y += 0.64f;
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
            Vector3 dir = Quaternion.AngleAxis(thetaMultiplier * 180f, Vector3.back) * Vector3.left;
            rb.AddForce(dir * this.ballInitialVelocity);
        }
    }
}
