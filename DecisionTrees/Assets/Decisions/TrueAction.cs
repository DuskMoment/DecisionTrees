using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrueAction : TreeNode
{
    public TrueAction(GameObject obj) : base(obj) { }
    public override bool decision()
    {
        Debug.Log("Used a true action");
        return true;
    }
}
