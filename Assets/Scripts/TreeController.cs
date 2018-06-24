using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {
    public Sprite[] levels;
    private int levelCap = 1;
    private int level = 1;
    private int levelCost = 5;
    private SpriteRenderer spriteR;

    void Start () {
        this.levelCap = this.levels.Length;
        this.spriteR = gameObject.GetComponent<SpriteRenderer>();
    }
	
	void Update () {
		
	}

    public int GetLevelCost()
    {
        return this.levelCost;
    }

    public bool CanLevelUp()
    {
        return this.level < this.levelCap;
    }

    public void LevelUp()
    {
        this.level = Mathf.Clamp(this.level+1, 1, this.levelCap);
        this.spriteR.sprite = this.levels[this.level-1];
    }
}
