using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public static FlockManager FM;

    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swimLimits = new Vector3(5,5,5);

    [Header("Fish Settings")]
    [Range(0, 5)]
    public float minSpeed;
    [Range(0, 5)]
    public float maxSpeed;
    [Range(1, 10)]
    public float neighbourDistance;
    [Range(1, 5)]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x), 
                                                                Random.Range(-swimLimits.y, swimLimits.y), 
                                                                Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = Instantiate(fishPrefab, pos, Quaternion.identity);
        }

        FM = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}