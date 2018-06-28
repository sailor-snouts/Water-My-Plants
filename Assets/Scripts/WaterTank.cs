using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour
{
    public int max = 200;
    public Transform mask;
    private int current = 200;
    private float fill = 1f;
    private float yVelocity = 0.0F;
    private float fillRate = 0.35f;

    void Start ()
    {
		
	}

    
	
	void Update ()
    {
        float amountToFillThisFrame = Mathf.SmoothDamp(this.mask.localScale.y, this.fill, ref yVelocity, fillRate);
        this.mask.localScale = new Vector3(1, amountToFillThisFrame, 1);
	}

    void SetFill()
    {
        this.fill = Mathf.InverseLerp(0, this.max, this.current);
    }

    public void AddWater(int amount)
    {
        this.current = Mathf.Clamp(this.current + amount, 0, this.max);
        this.SetFill();
    }

    public void UseWater(int amount)
    {
        this.current = Mathf.Clamp(this.current - amount, 0, this.max);
        this.SetFill();
    }

    public int GetWaterAmount()
    {
        return this.current;
    }

    public int GetWaterCapacity()
    {
        return this.max;
    }

    public bool HasWater()
    {
        return this.current > 0;
    }

    public bool HasWater(int amount)
    {
        return this.current >= amount;
    }
}
