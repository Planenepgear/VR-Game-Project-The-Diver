using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneDetection : MonoBehaviour
{
    public GameObject shell;
    public GameObject door;
    [SerializeField] private int boneNumber = 0;
    [SerializeField] private int stoneNumber = 0;
    private bool isLocked = true;

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

        if (collision.transform.CompareTag("Stone"))
        {
            stoneNumber++;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Stone"))
        {
            isLocked = true;
            shell.SetActive(true);
        }
        else
        {
            isLocked = false;
            if (shell)
            {
                shell.SetActive(false);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Bone"))
        {
            boneNumber -= 1;
        }

        if (collision.transform.CompareTag("Stone"))
        {
            stoneNumber--;
        }
    }

    private void Update()
    {
        if (stoneNumber <= 0 || !isLocked)
        {
            if (shell)
            {
                shell.SetActive(false);
            }
        }

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
