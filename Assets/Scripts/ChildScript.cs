using System;
using UnityEngine;

public class ChildBehavior : MonoBehaviour
{
    //you dont want everything to access the variable if it's public or else you could mess something up
    //use them from another script
    float fullnessVal = 5f;
    float hpVal = 5f;
    
    float needsTime;
    public float needsTimeReset;
    public float needsTimeStep;

    float waitTime;
    public float waitTimeReset;
    public float waitTimeStep;
    
    //the way we're writing it, makes it want to access something that's directly in the game
    public GameManager myManager;

    Vector3 targetPos;
    bool moving;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        needsTime = needsTimeReset;
        waitTime  = waitTimeReset;
        //public changes the scope or variable of the thing we're calling
    }

    // Update is called once per frame
    void Update()
    {
        needsTime -= needsTimeStep * Time.deltaTime;
        if (needsTime < 0)
        {
            IncrementNeeds();
        }

        if (moving)
        {
            //move towards takes two points and moves between them
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 2f * Time.deltaTime);
        }
    }

    void IncrementNeeds()
    {
        fullnessVal -= 1;
        needsTime = needsTimeReset;
        Debug.Log(fullnessVal);
        if (fullnessVal <= 0)
        {
            FindFood();
        }
    }

    void FindFood()
    {
        float dist = 2000f;
        GameObject closestFood = null;
        
        //for loop: it'll go through each item in the list 
        foreach (GameObject food in myManager.allFood)
        {
            if (Vector3.Distance(transform.position, food.transform.position) < dist)
            {
                dist = Vector3.Distance(transform.position, food.transform.position);
                closestFood = food;
            }
        }
        //GO TO THE FOOD

        if (closestFood != null)
        {
            targetPos = closestFood.transform.position;
            moving = true;
        }

        if (closestFood == null)
        {
            waitTime -= waitTimeStep * Time.deltaTime;
            if (waitTime <= 0)
            {
                Debug.Log(hpVal);
                hpVal -= 1;
                if (hpVal <= 0)
                {
                    Debug.Log("GameOver");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BabyFood"))
        {
            Debug.Log("Touching");
            myManager.allFood.Remove(other.gameObject);
            Destroy(other.gameObject);
            fullnessVal = 5;
        }
    }
    
}

