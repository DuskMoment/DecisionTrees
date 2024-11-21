using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTriggering : MonoBehaviour
{
    private bool triggered;
    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit somthing");
        triggered = true;
        col = collision;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("left hiting somthing");
        triggered = false;
        col = collision;
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
