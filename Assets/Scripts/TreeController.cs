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
    private Animator anim;
    private ForestController forest;

    public void Start () {
        this.levelCap = this.levels.Length;
        this.anim = gameObject.GetComponent<Animator>();
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
        this.SetLevel(this.level - 1);
    }

    public void LevelUp()
    {
        this.SetLevel(this.level + 1);
    }

    private void SetLevel(int level)
    {
        level = Mathf.Clamp(level, 1, this.levelCap);

        if (level < this.level)
        {
            this.forest.TreeDowngraded();
        }
        if (this.level == this.levelCap)
        {
            this.forest.TreeCompleted();
        }

        this.level = level;      
        this.anim.SetInteger("Level", level);
        this.timeUntilDowngrade = this.level * this.downgradeTimer;
    }
}
