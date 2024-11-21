using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderFunction : MonoBehaviour
{
    [SerializeField] int startEnergy;
    private bool isWandering = false;
    [SerializeField] int decayRate = 1;
    [SerializeField] float timer = 1;
    float timeLeft = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(isWandering)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                //decay
                timeLeft = timer;
                startEnergy -= decayRate;
            }
        }
        else
        {
            timeLeft = timer;
        }
        
        
    }

    public void setWandering(bool w)
    {
        isWandering = w;
    }

    public bool getWanderState()
    {
        return isWandering;

    }

    public bool isOutOfEnergy()
    {
        if (startEnergy < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void increaseEnergy()
    {
        startEnergy++;
    }

}
