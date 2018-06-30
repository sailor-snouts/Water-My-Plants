using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {
    private float amplitude = 0.5f;
    private float period = 3;
    private float velocity = 1;
    private float dir = 1;
    private float positionX = 10;
    private float offsetY = 0;
    private float positionY = 0;
    public float waitTime = 1f;
    private bool isWaiting = false;

	void Start () {
        this.positionX = this.transform.position.x;
        this.positionY = this.transform.position.y;
        this.offsetY = this.positionY;

        if(Random.value > 0.5f)
        {
            this.dir *= 1;
        }
        else
        {
            this.dir *= -1;
        }
    }
	
	void Update () {
        if(this.isWaiting)
        {
            this.Waiting();
            return;
        }

        if((this.dir < 0f && this.positionX < -6f) || (this.dir > 0f && this.positionX > 4f))
        {
            this.waitTime = Random.Range(1f, 2f);
            this.isWaiting = true;
            return;
        }

        this.positionX += this.velocity * this.dir  * Time.deltaTime;
        this.positionY = this.offsetY + Mathf.Sin(this.period * Time.time) * this.amplitude;
        this.transform.position = new Vector3(this.positionX, this.positionY, 0);
    }

    void Waiting()
    {
        waitTime -= Time.deltaTime;

        if(waitTime < 0f)
        {
            this.positionX = this.transform.position.x;
            this.positionY = this.transform.position.y;
            this.offsetY = this.positionY;
            this.isWaiting = false;
            this.ChangeDirection();
        }

        this.positionY = this.offsetY + Mathf.Sin(this.period * Time.time) * this.amplitude;
        this.transform.position = new Vector3(this.positionX, this.positionY, 0);
    }

    void ChangeDirection()
    {
        this.dir *= -1;
        this.amplitude = Random.Range(0f, 1f);
        this.period = Random.Range(1f, 3f);
        this.velocity = Random.Range(1f, 3f);
        Vector3 theScale = transform.localScale;
        theScale.x *= this.dir;
        transform.localScale = theScale;
    }
}
