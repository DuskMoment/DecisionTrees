using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerButton : TreeNode
{
    GameObject lockedObj;
    OnTopOfFruit mData;
    public TriggerButton(GameObject obj, OnTopOfFruit data) : base(obj)
    {
        mData = data;

    }
    public override bool decision()
    {
        //get the button code to despawn the object
        Collider2D col = mGameObject.GetComponent<CheckForTriggering>().getCollision();
        WanderFunction wander = mGameObject.GetComponent<WanderFunction>();

        if(mData.lockedObj != null)
        {
            //use the locked object for deleting
            lockedObj = mData.lockedObj;
        }
        else
        {
            Debug.LogWarning("changed object");
            lockedObj = col.gameObject;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            //kill gameobj and return true
            wander.increaseEnergy();
            GameObject.Destroy(lockedObj);

        }
       return false;
    }
}
