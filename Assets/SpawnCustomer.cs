using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomer : MonoBehaviour
{
    public GameObject[] customers;
    [SerializeField] private int maxDeployed;
    public Transform[] deployedPositions;
    public bool[] positionsFilled;

    private float timeElapsed = 0f;
    private readonly float timeBetweenSpawns = 3f;
    // Start is called before the first frame update
    void Start()
    {
        maxDeployed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeBetweenSpawns)
        {
            timeElapsed = 0;
            int availabilityIndex = IsSpaceAvailable();
            if (availabilityIndex != -1)
            {
                SpawnTheCustomer(availabilityIndex);
                positionsFilled[availabilityIndex] = true;
            }
        }

    }

    void SpawnTheCustomer(int position) {
        GameObject toSpawn;
        if (customers.Length > 0)
        {
            int randNum = Random.Range(0, customers.Length); // spawn random object from customers array
            toSpawn = Instantiate(customers[randNum], transform.position, Quaternion.identity);

            Customer customerScript = toSpawn.GetComponent<Customer>();
            if (customerScript != null) // we successfully got the component
            {
                customerScript.SetGoal(deployedPositions[position]);
            }
            else
            {
                Adventurer adventurerScript = toSpawn.GetComponent<Adventurer>();
                if (adventurerScript != null)
                {
                    adventurerScript.SetGoal(deployedPositions[position]);
                }
                else
                {
                    Debug.Log("Spawned an object that doesn't have the correct script oh fuck please don't fire me I need this job");
                }
            }
        }
    }

    // returns the first available index if there is space in any of the positions
    int IsSpaceAvailable() {
        for (int i = 0; i < positionsFilled.Length; i++)
        {
            if (!positionsFilled[i]) // if this position is not filled, it is available
            {
                return i;
            }
        }
        return -1; // if we've reached this point it means all the positions are filled
    }
}
