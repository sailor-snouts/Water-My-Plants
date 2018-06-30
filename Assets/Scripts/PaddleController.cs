using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {
    public float minX = -6f;
    public float maxX = -6f;
    private float y;
    private float z;
    private Vector3 pointerPosition;
    private float moveTo;
    Rigidbody2D rigidBody;
    [SerializeField]
    float controllerInputForceMultiplier = 65;
    private bool useMouse = true;

    void Start () {
        this.y = this.transform.position.y;
        this.z = this.transform.position.z;
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        /**
         * Tweak feel of paddle when using controller by changing the force multiplier here,
         * and the linear drag on the paddle's rigidBody2D.
         */
        float controllerInput = Input.GetAxis("Horizontal");
        if (controllerInput != 0) {
            rigidBody.AddForce(new Vector2(controllerInput * controllerInputForceMultiplier, this.y));
            useMouse = false;
        } else if(useMouse) {
            /**
             * Mouse can't be on when controller is active, otherwise it will constantly reset the position
             * of the paddle, even when the mouse is offscreen.
             */
            this.pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.moveTo = Mathf.Clamp(this.pointerPosition.x, this.minX, this.maxX);
            this.transform.position = new Vector3(moveTo, this.y, this.z);
        }
    }
}
