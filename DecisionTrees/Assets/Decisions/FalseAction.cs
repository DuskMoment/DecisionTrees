using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseAction : TreeNode
{
    public FalseAction(GameObject obj) : base(obj) { }
    public override bool decision()
    {
        Debug.Log("Used a false action");
        return false;
    }
}