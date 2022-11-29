using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public float speedStand = 1f;
    [HideInInspector] public float speed = 1f;
    float rotationSpeed = 2.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 5.0f;
    bool turning = false;

    public GameObject manager;
    private GlobalFlock globalFlock;

    // Use this for initialization
    void Start()
    {
        speed = Random.Range(speedStand * 0.5f, speedStand);
        globalFlock = manager.GetComponent<GlobalFlock>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(transform.position, Vector3.zero) >= GlobalFlock.tankSize)
        if (Vector3.Distance(transform.position, manager.transform.position) >= globalFlock.tankSize)
        {
            turning = true;
        }
        else
            turning = false;

        if (turning)
        {
            //Vector3 direction = Vector3.zero - transform.position;
            Vector3 direction = manager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime);
            speed = Random.Range(speedStand * 0.5f, speedStand);
        }
        else
        {
            if (Random.Range(0, 5) < 3)
                ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);

    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = globalFlock.allFish;

        //Vector3 vcentre = Vector3.zero;
        Vector3 vcentre = manager.transform.position;
        //Vector3 vavoid = Vector3.zero;
        Vector3 vavoid = manager.transform.position;

        float gSpeed = 0.1f;

        Vector3 goalPos = globalFlock.goalPos;

        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed +=  anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            //if (direction != Vector3.zero)
            if (direction != manager.transform.position)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);
        }
    }
}
