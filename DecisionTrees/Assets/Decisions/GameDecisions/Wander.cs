using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Wander : TreeNode
{
   public Wander(GameObject obj) : base(obj)
    {

    }
    public override bool decision()
    {
        WanderFunction wander = mGameObject.GetComponent<WanderFunction>();

        Assert.IsNotNull(wander);

        //set the player to wander
        wander.setWandering(true);
       
        return !wander.isOutOfEnergy();
    }
}
