using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsColiding : TreeNode
{
    // Start is called before the first frame update
    public IsColiding(GameObject obj) : base(obj)
    {

    }

    public override bool decision()
    {
        return checkForCollision();
    }

    private bool checkForCollision()
    {
        Transform trans = mGameObject.transform;

        CheckForColiding col = mGameObject.GetComponent<CheckForColiding>();

        if (col == null)
        {
            Debug.Log("collsion object was null returing false");
            return false;
        }

        //returns the collison 
        return col.getIsColiding();
        
    }
}
