using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForColiding : MonoBehaviour
{
    private bool colided;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit somthing");
        colided = true;
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("left hiting somthing");
        colided = false;
    }
    public bool getIsColiding()
    {
        return colided;
    }
}
