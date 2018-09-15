using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CloudController : MonoBehaviour
{

    [SerializeField]
    private float magnitude = 1f;

    [SerializeField]
    private float boundTop;
    [SerializeField]
    private float boundRight;
    [SerializeField]
    private float boundBottom;
    [SerializeField]
    private float boundLeft;

    [SerializeField]
    private Vector2 goal;


    [SerializeField]
    private float waitStartPosition;
    [SerializeField]
    private float waitStartTime;
    [SerializeField]
    private float waitMin;
    [SerializeField]
    private float waitMax;
    [SerializeField]
    private float waitCounter;

    private Rigidbody2D rb;

    private void Awake()
    {
        this.rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.NewGoal();
    }

    private void Update()
    {
        if(this.waitCounter > 0f)
        {
            this.waitCounter -= Time.deltaTime;
            if(this.waitCounter < 0f)
            {
                this.NewGoal();
            }
            else
            {
                this.transform.position = new Vector2(this.transform.position.x, this.waitStartPosition + 0.5f * Mathf.Sin((Time.time - this.waitStartTime) * 2f));
            }

        }
        else if(Mathf.Abs(this.transform.position.x - this.goal.x) < 0.1f && Mathf.Abs(this.transform.position.y - this.goal.y) < 0.1f)
        {
            this.rb.velocity = Vector3.zero;
            this.waitCounter = Random.Range(this.waitMin, this.waitMax);
            this.waitStartPosition = this.transform.position.y;
            this.waitStartTime = Time.time;
        }
    }

    private void NewGoal()
    {
        this.goal.x = Random.Range(this.boundLeft, this.boundRight);
        this.goal.y = Random.Range(this.boundBottom, this.boundTop);

        Vector2 dir = this.goal - (Vector2)this.transform.position;

        if(dir.magnitude < 2f)
        {
            this.NewGoal();
            return;
        }

        this.rb.velocity = Vector3.zero;
        rb.AddForce(dir.normalized * this.magnitude);
    }
}
