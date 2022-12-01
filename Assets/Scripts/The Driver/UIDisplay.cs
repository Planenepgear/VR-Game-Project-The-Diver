using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplay : MonoBehaviour
{
    public float spawanDistance = 2;
    private GameObject head;
    private Transform headTransform;
    private GameObject originParent;

    private void Awake()
    {
        originParent = transform.parent.gameObject;
    }

    private void OnEnable()
    {
        head = GameObject.FindGameObjectWithTag("MainCameraCharacter");
        transform.SetParent(originParent.transform.parent);
    }

    //private void OnDisable()
    //{
    //    transform.SetParent(originParent.transform);
    //}

    void Update()
    {
        if (head)
        {
            headTransform = head.transform;

            if (gameObject.activeInHierarchy == true)
            {
                transform.position = headTransform.position +
                    new Vector3(headTransform.forward.x, headTransform.forward.y, headTransform.forward.z).normalized * spawanDistance;

                transform.LookAt(headTransform.position);
                transform.forward *= -1;
            }
        }
    }
}
