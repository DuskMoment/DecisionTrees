using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnergy : MonoBehaviour
{
    [SerializeField] GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        var pos = (Vector2)transform.position;
        Vector2 offset = transform.localScale;

       var invPos = pos - offset;
       //top left
       pos += offset;

        for(int i = 0; i < 4; i++) 
        {
            float x = Random.Range(invPos.x, pos.x);
            float y = Random.Range(invPos.y, pos.y);

            Vector2 newPos = new Vector2(x, y);
            Instantiate(spawn, newPos, Quaternion.identity);
        }


       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
