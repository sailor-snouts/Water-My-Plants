using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {
    private int levelCap = 4;
    public int level = 1;
    private int levelCost = 1;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public bool CanLevelUp()
    {
        return this.level < this.levelCap;
    }

    public void LevelUp()
    {
        this.level += 1;
    }
}
