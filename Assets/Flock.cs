using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) < 10)
            speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);

        if (Random.Range(0, 100) < 10)
            ApplyRules();

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = FlockManager.FM.allFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, transform.position);
                if(nDistance <= FlockManager.FM.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (nDistance < 1)
                    {
                        vavoid = vavoid + (transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (FlockManager.FM.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            if(speed > FlockManager.FM.maxSpeed)
            {
                speed = FlockManager.FM.maxSpeed;
            }

            Vector3 dir = (vcentre + vavoid) - transform.position;
            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), FlockManager.FM.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
