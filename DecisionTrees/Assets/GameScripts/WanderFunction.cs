using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderFunction : MonoBehaviour
{
    [SerializeField] int startEnergy;
    private bool isWandering = false;
    [SerializeField] int decayRate = 1;
    [SerializeField] float timer = 4;
    float timeLeft = 0;
    Vector2 wanderDirection = Vector2.zero;

    Rigidbody2D mov = null;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timer;
        mov = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isWandering)
        {
            Debug.Log("Wandering");
            wanderMovemnt();
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
            wanderDirection = getRandDirection();
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
            Debug.Log("Out of endergy");
            return true;
        }
        else
        {
            return false;
        }
    }
    public int getCurrentEnergy()
    {
        return startEnergy; 
    }
    public void increaseEnergy()
    {
        Debug.Log("energyIncreased");
        startEnergy++;
    }

    private void wanderMovemnt()
    {
        
        mov.velocity = wanderDirection.normalized;

    }
    public void setWanderMovment(Vector2 vec)
    {
        mov.velocity = vec;
    }
    private Vector2 getRandDirection()
    {
        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);
        return new Vector2(x, y);
    }
}
