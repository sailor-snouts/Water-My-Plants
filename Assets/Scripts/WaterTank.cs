using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour
{
    public int max = 100;
    public Transform mask;
    private int current = 100;
    private float fill = 1f;
    
    void Start ()
    {
		
	}
	
	void Update ()
    {
	}

    void SetFill()
    {
        this.fill = (float) this.current / (float) this.max;
        this.mask.localScale = new Vector3(1, this.fill, 1);
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
