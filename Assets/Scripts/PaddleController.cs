using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {
    public float minX = -6f;
    public float maxX = -6f;
    private float y;
    private float z;
    private Vector3 pointerPosition;
    [SerializeField]
    private float force = 0f;
    private float moveTo;
    private bool isGamepad = false;
    private float controllerAxis;
    Rigidbody2D rigidBody;

    [SerializeField]
    float controllerInputForceMultiplier = 65;

    void Start () {
        this.y = this.transform.position.y;
        this.z = this.transform.position.z;
        rigidBody = GetComponent<Rigidbody2D>();
        this.isGamepad = Input.GetJoystickNames().Length > 0;
    }
	
	void Update ()
    {
        if (this.isGamepad)
        {
            this.controllerAxis = Input.GetAxis("Horizontal");
            if(Mathf.Abs(this.controllerAxis) > 0.1f)
            {
                this.force = this.controllerAxis * this.controllerInputForceMultiplier * Time.deltaTime;
            }
            else if(Mathf.Abs(this.force) > 0.01f)
            {
                this.force *= 0.8f;
            }
            else
            {
                this.force = 0f;
            }

            this.moveTo = this.transform.position.x + this.force;
            this.moveTo = Mathf.Clamp(this.moveTo, this.minX, this.maxX);
            this.transform.position = new Vector3(moveTo, this.y, this.z);
        }
        else
        {
            this.pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.moveTo = Mathf.Clamp(this.pointerPosition.x, this.minX, this.maxX);
            this.transform.position = new Vector3(moveTo, this.y, this.z);
        }
    }
}
