using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Wander : TreeNode
{
    WanderFunction wander;
   public Wander(GameObject obj) : base(obj)
    {
        wander = mGameObject.transform.GetComponent<WanderFunction>();
    }
    public override bool decision()
    {
        //WanderFunction wander = mGameObject.GetComponent<WanderFunction>();

        Assert.IsNotNull(wander);

        //if we where not prev wandering set the wandering to zero to not have build up of things
        if(wander.getWanderState() != true)
        {
            wander.setWanderMovment(Vector2.zero);
        }
        //set the player to wander
        wander.setWandering(true);

        if(wander.isOutOfEnergy()) 
        {
            //retrun false to die
            Debug.Log("Out of endergy");
            return false;
        }
        //remember to fix this
        return true; //!wander.isOutOfEnergy();
    }
}
