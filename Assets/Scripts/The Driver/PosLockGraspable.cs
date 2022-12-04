using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosLockGraspable : MonoBehaviour
{
    [SerializeField] float minX = -20.0f;
    [SerializeField] float maxX = 20.0f;

    [SerializeField] float minY = -20.0f;
    [SerializeField] float maxY = 20.0f;

    [SerializeField] float minZ = -20.0f;
    [SerializeField] float maxZ = 20.0f;

    void Update()
    {
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, minX, maxX), 
            Mathf.Clamp(transform.localPosition.y, minY, maxY), Mathf.Clamp(transform.localPosition.z, minZ, maxZ));
    }
}
