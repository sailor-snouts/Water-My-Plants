using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public int levelCost = 2;
    public Sprite[] levels;
    private float timeUntilDowngrade;
    private float downgradeTimer = 8f;
    private int levelCap = 4;
    private int level = 1;
    private SpriteRenderer spriteR;
    private ForestController forest;

    public void Start () {
        this.levelCap = this.levels.Length;
        this.spriteR = gameObject.GetComponent<SpriteRenderer>();
        this.forest = this.GetComponentInParent<ForestController>();
    }
	
	void Update () {
		if(this.level > 1)
        {
            this.timeUntilDowngrade -= Time.deltaTime;
            if(timeUntilDowngrade <= 0)
            {
                this.LevelDown();
            }
        }
	}

    public int GetLevelCost()
    {
        return this.levelCost;
    }

    public bool CanLevelUp()
    {
        return this.level < this.levelCap;
    }

    public void LevelDown()
    {
        if (this.level == this.levelCap)
        {
            this.forest.TreeDowngraded();
        }

        this.level = Mathf.Clamp(this.level - 1, 1, this.levelCap);
        this.timeUntilDowngrade = this.level * this.downgradeTimer;

        this.spriteR.sprite = this.levels[this.level - 1];
    }

    public void LevelUp()
    {
        this.level = Mathf.Clamp(this.level+1, 1, this.levelCap);
        this.timeUntilDowngrade = this.level * this.downgradeTimer;

        if(this.level == this.levelCap)
        {
            this.forest.TreeCompleted();
        }   

        this.spriteR.sprite = this.levels[this.level - 1];
    }
}
