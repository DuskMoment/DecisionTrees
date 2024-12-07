using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NearFruit : TreeNode
{
    CheckForTriggering check = null;
    Collider2D colData;
    bool hasTarget = false;
    Vector3 lockVec;
    GameObject lockedObject;
    public NearFruit(GameObject obj) : base(obj) 
    {
    
        check = mGameObject.GetComponent<CheckForTriggering>();
        //col = mGameObject.GetComponent<Collider2D>();   
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

            //we dont hava  currenct target the the object we where locked on to is inactive
            /* if(lockedObject == null || (hasTarget == false && lockedObject.activeSelf == false))
             {
                 Debug.LogWarning(" got a new target");
                 //store the first colision
                 lockVec = check.getCollision().gameObject.transform.position;
                 lockedObject = check.getCollision().gameObject;
             }*/


            var fruitPos = check.getCollision().gameObject.transform.position;
            Vector2 movVec = fruitPos - mGameObject.transform.position;
            //if the object is still alive then use it
            if (lockedObject != null) 
            {
                //Debug.LogWarning("used locked object");
                fruitPos = lockedObject.gameObject.transform.position;
                movVec = fruitPos - mGameObject.transform.position;
            }
            else
            {
                lockedObject = check.getCollision().gameObject;
            }
            

            mov.velocity = movVec.normalized;
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
            hasTarget = false;
            return false; 
        }

    }
}
