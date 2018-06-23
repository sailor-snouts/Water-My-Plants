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
	
	void Update () {
        if (Input.GetButtonDown("Fire1") && ballInPlay == false)
        {
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(0, ballInitialVelocity, 0));
        }
    }
}
