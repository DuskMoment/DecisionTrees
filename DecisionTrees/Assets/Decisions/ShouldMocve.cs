using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldMocve : TreeNode
{
    bool testMember = false;

    public ShouldMocve(GameObject obj) : base(obj) { }
    public override bool decision()
    {
        
        return false;
        
    }
}
