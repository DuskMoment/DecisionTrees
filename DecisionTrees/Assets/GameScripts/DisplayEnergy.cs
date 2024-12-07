using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayEnergy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] WanderFunction wander;
    // Start is called before the first frame update
    void Start()
    {
        text.text = wander.getCurrentEnergy().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (wander.getCurrentEnergy() >= 0)
        {
            text.text = wander.getCurrentEnergy().ToString();
        }
        else
        {
            text.text = "You have Died :(";
        }
        

        
    }
}
