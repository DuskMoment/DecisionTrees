using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerButton : TreeNode
{
    public TriggerButton(GameObject obj) : base(obj)
    {

    }
    public override bool decision()
    {
        //get the button code to despawn the object
        Collider2D col = mGameObject.GetComponent<CheckForTriggering>().getCollision();
        WanderFunction wander = mGameObject.GetComponent<WanderFunction>();
        if (Input.GetMouseButtonDown(0))
        {
            //kill gameobj and return true
            wander.increaseEnergy();
            GameObject.Destroy(col.gameObject);

        }
       return false;
    }
}
