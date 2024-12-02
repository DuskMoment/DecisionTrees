using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTopOfFruit : TreeNode
{
    public OnTopOfFruit(GameObject go) : base(go)
    {

    }
    public override bool decision()
    {

        var mov = mGameObject.GetComponent<Rigidbody2D>();
        var pos = mGameObject.GetComponent<CheckForTriggering>().getCollision().gameObject.transform.position;
        Debug.Log("doing ontop of fuit");

        if(Mathf.Pow((mov.position - (Vector2)pos).magnitude,2) < 0.25f)
        {
            Debug.Log("stopping in on top of the object");
            //on top of fruit
            mov.velocity = Vector3.zero;
            return true;
        }

        return false;
    }
}
