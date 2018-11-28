using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestController : MonoBehaviour
{
    private TreeController[] trees;
    private int treeCount;
    private int completedTrees;
    private int giftedLevels = 10;

    void Start()
    {
        this.trees = this.GetComponentsInChildren<TreeController>();
        this.treeCount = this.trees.Length;
        this.completedTrees = 0;

        for (int i = 0; i < this.giftedLevels; i++)
        {
            int luckyTree = Random.Range(1, this.treeCount) - 1;
            if (this.trees[luckyTree].gameObject.tag == "Flower")
            {
                i--;
                continue;
            }
            else
            {
                this.trees[luckyTree].Start();
                this.trees[luckyTree].LevelUp();
            }
        }
    }

    public void TreeCompleted()
    {
        this.completedTrees = Mathf.Clamp(this.completedTrees+1, 1, this.treeCount);
    }

    public void TreeDowngraded()
    {
        this.completedTrees = Mathf.Clamp(this.completedTrees - 1, 0, this.treeCount);
    }

    public bool CanTreesLevelUp()
    {
        return this.completedTrees <= this.treeCount;
    }

    public float GetCompletion()
    {
        return Mathf.Clamp01((float) this.completedTrees / (float) this.treeCount);
    }
}
