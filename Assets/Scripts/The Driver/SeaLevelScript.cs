using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaLevelScript : MonoBehaviour
{
    public Transform Scene;
    public float Ydistance = 10f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Scene.position.y + Ydistance, transform.position.z);
    }
}
