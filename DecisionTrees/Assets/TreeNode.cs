using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TreeNode
{
    //need this if I want to look at anything realted to that game throught the eyes of the chrachter and or do somthing reated to them
    protected GameObject gameObject;

    protected TreeNode(GameObject obj)
    {
        gameObject = obj;
    }

    public abstract bool decision();
   
}
