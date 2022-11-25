using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAnimationSwitch : MonoBehaviour
{
    public RuntimeAnimatorController characterController;
    public Avatar characterAvatar;
    public Transform HeadPos;

    private Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        this.transform.position = new Vector3(transform.position.x, HeadPos.position.y - 1.1f, transform.position.z);
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
        }

        if(isFirstPerson)
        {
            animator.runtimeAnimatorController = null;
            animator.avatar = null;

            this.GetComponent<VRRig>().enabled = true;
            this.GetComponent<RigBuilder>().enabled = true;
        }
    }
}
