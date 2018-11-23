using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public Transform mask;
    public SpriteRenderer flower;
    [SerializeField]
    private float fill = 0;
    private float current = 0;
    private float yVelocity = 0.0F;
    private float fillRate = 0.35f;
    
    void Update ()
    {
        float amountToFillThisFrame = Mathf.SmoothDamp(this.mask.localScale.y, this.fill, ref yVelocity, fillRate);
        this.mask.localScale = new Vector3(1, amountToFillThisFrame, 1);
        if (this.fill >= 0.99f)
        {
            flower.enabled = true;
        }
    }

    public void SetGoal(float completion)
    {
        this.current = Mathf.Clamp01(completion);
        this.fill = Mathf.InverseLerp(0, 1f, this.current);
    }
}
