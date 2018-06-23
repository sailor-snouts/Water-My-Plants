using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {
    public float minX = -9f;
    public float maxX = -9f;
    private float y;
    private float z;
    private Vector3 pointerPosition;
    private float moveTo;

	void Start () {
        this.y = this.transform.position.y;
        this.z = this.transform.position.z;
	}
	
	void Update () {
        this.pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.moveTo = Mathf.Clamp(this.pointerPosition.x, this.minX, this.maxX);
        this.transform.position = new Vector3(moveTo, this.y, this.z);
    }
}
