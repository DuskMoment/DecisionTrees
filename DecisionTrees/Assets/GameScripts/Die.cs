using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
   public void kill()
   {
        if (gameObject.activeSelf) 
        {
            //unlock the camera
            Camera.main.transform.parent = null;
            //destroy then object (just turn it off)
            gameObject.SetActive(false);
        }
   }

}
