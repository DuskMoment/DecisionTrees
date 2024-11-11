using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueAction : TreeNode
{
    public override bool decision()
    {
        Debug.Log("Used a true action");
        return true;
    }
}
