using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneDetection : MonoBehaviour
{
    public GameObject shell;
    public GameObject door;
    [SerializeField] private int boneNumber = 0;

    private void Start()
    {
        boneNumber = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bone"))
        {
            boneNumber += 1;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Stone"))
        {
            shell.SetActive(true);
        }
        else
        {
            shell.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Bone"))
        {
            boneNumber -= 1;
        }
    }

    private void Update()
    {
        if(boneNumber >= 5)
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }
}
