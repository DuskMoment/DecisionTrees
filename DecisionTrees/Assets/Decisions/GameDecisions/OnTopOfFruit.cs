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

        if(mov.position.magnitude < 0.5f)
        {
            //on top of fruit
            mov.velocity = Vector3.zero;
            return true;
        }

        return false;
    }
}
