using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTopOfFruit : TreeNode
{
    Rigidbody2D mov;
    CheckForTriggering trigger;
    public OnTopOfFruit(GameObject go) : base(go)
    {
        mov = mGameObject.GetComponent<Rigidbody2D>();
        trigger = mGameObject.GetComponent<CheckForTriggering>();

    }
    public override bool decision()
    {

        //var mov = mGameObject.GetComponent<Rigidbody2D>();
        var pos = trigger.getCollision().gameObject.transform.position;
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
