using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    Transform goal;
    bool goalReached;
    float reachedPrecision = 0.05f;
    float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!goalReached)
        {
            MoveTowardsGoal();
            float distance = Vector2.Distance(transform.position, goal.transform.position);
            if (distance <= reachedPrecision)
            {
                goalReached = true;
            }
        }
    }

    void MoveTowardsGoal() {
        transform.position = Vector2.MoveTowards(transform.position, goal.position, moveSpeed * Time.deltaTime);
    }

    public void SetGoal(Transform toSet) {
        goal = toSet;
        goalReached = false;
    }
}
