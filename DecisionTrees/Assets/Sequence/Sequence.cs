using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

[System.Serializable]
public abstract class Sequence : TreeNode
{

    public struct ReturnData
    {
        public int lastAccsedIndex;
        public bool didPass;
        public ReturnData(int index)
        {
            lastAccsedIndex = index;
            didPass = true;
        }

    };
    protected List<TreeNode> children = new List<TreeNode>();
    protected Sequence(GameObject obj) : base(obj) { }

    public abstract ReturnData evaluateChildren();

    public void addChildren(List<TreeNode> toAdd)
    {
        if (children.Count <= 0)
        {
            children = toAdd;
        }
        else
        {
            children.AddRange(toAdd);
        }
    }

    public void addChildren(TreeNode toAdd)
    {
        children.Add(toAdd);

    }
    
   

}
