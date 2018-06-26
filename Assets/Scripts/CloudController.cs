using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {
    private float amplitude = 0.5f;
    private float period = 3;
    private float dir = 1;
    private float positionX = 10;
    private float offsetY = 0;
    private float positionY = 0;

	void Start () {
        this.amplitude = Random.Range(0f, 1f);
        this.period = Random.Range(1f, 3f);
        this.dir = Random.Range(1f, 3f);
        this.offsetY = Random.Range(-2f, 3f);

        if(Random.value > 0.5f)
        {
            this.positionX = Random.Range(-20f, -15f);
            this.dir *= 1;
        }
        else
        {
            this.positionX = Random.Range(15f, 20f);
            this.dir *= -1;
        }
    }
	
	void Update () {
        if((this.dir < 0f && this.positionX < -20f) || (this.dir > 0f && this.positionX > 20f))
        {
            Destroy(this.gameObject);
        }

        this.positionX += this.dir * Time.deltaTime;
        this.positionY = this.offsetY + Mathf.Sin(this.period * Time.time) * this.amplitude;
        this.transform.position = new Vector3(this.positionX, this.positionY, 0);
    }
}
