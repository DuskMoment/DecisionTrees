using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldDie : TreeNode
{
    Die deathHandler;
    public ShouldDie(GameObject obj) : base(obj)
    {
        deathHandler = obj.GetComponent<Die>();
    }
    public override bool decision()
    {
        if (deathHandler != null) 
        {
            deathHandler.kill();
        }

        return true;
        
    }

}
