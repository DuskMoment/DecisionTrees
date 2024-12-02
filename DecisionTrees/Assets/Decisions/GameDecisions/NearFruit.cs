using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NearFruit : TreeNode
{
    CheckForTriggering check = null;
    public NearFruit(GameObject obj) : base(obj) 
    {
    
        check = mGameObject.GetComponent<CheckForTriggering>();

        Assert.IsNotNull(check);
    }
    public override bool decision()
    {
        //check to see if we are near somthing
        if(check.getIsTriggering())
        {
            
            //rememvber to set wanding to false
            var wander = mGameObject.GetComponent<WanderFunction>(); //.setWandering(false);
            
            if(wander.getWanderState())
            {
                //if we where wandering and if we found a thing stop wandering and reset speed to zero
                wander.setWandering(false);
                wander.setWanderMovment(Vector2.zero);
            }
            



            var mov =  mGameObject.GetComponent<Rigidbody2D>();

            //our pos minus the fruit

            var fruitPos = check.getCollision().gameObject.transform.position;
            Vector2 movVec = fruitPos - mGameObject.transform.position;

            mov.AddForce(movVec.normalized);
            //maybe add this to a diffrent decsion like is ontop of thing
            //if (movVec.magnitude > 0.5f)
            //{
            //    mov.AddForce(movVec.normalized);
            //}
            //else
            //{
            //    //stop moving 
            //    mov.velocity = Vector2.zero;
            //}

            Debug.Log("Moving toward object " + check.getCollision().name);
            return true;
        }
        else 
        {
            return false; 
        }

    }
}
