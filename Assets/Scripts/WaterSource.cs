using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour {
    public int max = 100;
    private int amount;
    private SpriteRenderer spriteR;

	void Start () {
        this.spriteR = this.GetComponent<SpriteRenderer>();
        this.amount = this.max;
    }

    void Update()
    {
        if (this.amount <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        Color tmp = this.spriteR.color;
        tmp.a = (float) amount / (float) max;
        this.spriteR.color = tmp;
    }

    public int GetWater(int capacity)
    {
        int amount = Mathf.Min(capacity, this.amount);
        this.amount -= amount;
    
        return amount;
    }
}
