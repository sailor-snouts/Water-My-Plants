using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestController : MonoBehaviour
{
    public TreeController[] trees;

    void Start()
    {
        
    }

    void Update()
    {

    }


    public bool CanTreesLevelUp()
    {
        foreach (TreeController tree in trees)
        {
            if (tree.CanLevelUp())
            {
                return true;
            }
        }

        return false;
    }


    public int MaxedOutTrees()
    {
        int count = 0;
        foreach (TreeController tree in trees)
        {
            if (!tree.CanLevelUp())
            {
                count++;
            }
        }

        return count;
    }
}
