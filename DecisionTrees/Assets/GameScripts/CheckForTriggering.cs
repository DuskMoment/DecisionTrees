using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTriggering : MonoBehaviour
{
    private bool triggered;
    private Collider2D col;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit somthing");
        triggered = true;
        col = collision;

    }
    public void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("left hiting somthing");
        triggered = false;
        col = collision;
        //chache size then make it big a again this will hope fully reset game

        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<Collider2D>().enabled = true;


    }
    public bool getIsTriggering()
    {
        return triggered;
    }
    public Collider2D getCollision()
    {
        return col;
    }
}
