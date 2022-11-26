using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CharacterAnimationSwitch : MonoBehaviour
{
    public RuntimeAnimatorController characterController;
    public Avatar characterAvatar;
    public Transform HeadPos;

    public GameObject HipL;
    public GameObject HipR;

    private Animator animator;
    //private float savedMoveSpeed;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        this.transform.position = new Vector3(transform.position.x, HeadPos.position.y - 2f, transform.position.z);
        //savedMoveSpeed = this.transform.parent.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed;
    }

    public void FirstPerson(bool isFirstPerson)
    {
        if (!isFirstPerson)
        {
            animator.runtimeAnimatorController = characterController;
            animator.avatar = characterAvatar;

            this.GetComponent<VRRig>().enabled = false;
            this.GetComponent<RigBuilder>().enabled = false;

            this.transform.position = new Vector3(transform.position.x, HeadPos.position.y - 1f, transform.position.z);
            //this.transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);

            //this.transform.parent.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = savedMoveSpeed;
            this.transform.parent.GetComponent<Swimming3rd>().enabled = true;

            HipL.transform.localScale = Vector3.one;
            HipR.transform.localScale = Vector3.one;
        }

        if(isFirstPerson)
        {
            animator.runtimeAnimatorController = null;
            animator.avatar = null;

            this.GetComponent<VRRig>().enabled = true;
            this.GetComponent<RigBuilder>().enabled = true;

            //this.transform.parent.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = 0;
            this.transform.parent.GetComponent<Swimming3rd>().enabled = false;

            HipL.transform.localScale = Vector3.zero;
            HipR.transform.localScale = Vector3.zero;
        }
    }
}
