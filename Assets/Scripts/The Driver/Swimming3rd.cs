using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Swimming3rd : MonoBehaviour
{
    public InputActionProperty LTrigger;
    public InputActionProperty RTrigger;

    public InputActionProperty LMoveaction;
    private Rigidbody rb;

    public GameObject characterModle;

    private float SpeedDecay = 1;

    private Animator animator;
    private int isSwimmingHash;

    void Start()
    {
        animator = characterModle.GetComponent<Animator>();
        isSwimmingHash = Animator.StringToHash("isSwimming");

        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool isSwimming = animator.GetBool(isSwimmingHash);

        if (LTrigger.action.ReadValue<float>() > 0.1f)
        {
            rb.AddForce(1.25f * LMoveaction.action.ReadValue<Vector2>().sqrMagnitude * transform.up);
        }
        else if (RTrigger.action.ReadValue<float>() > 0.1f)
        {
            rb.AddForce(1.25f * LMoveaction.action.ReadValue<Vector2>().sqrMagnitude * -transform.up);
        }

        if (rb.velocity.sqrMagnitude > 0.001f)
        {
            rb.AddForce(-rb.velocity * SpeedDecay);
        }

        if (LMoveaction.action.ReadValue<Vector2>().sqrMagnitude > 0.01f && !isSwimming)
        {
            animator.SetBool(isSwimmingHash, true);
        }
        if (LMoveaction.action.ReadValue<Vector2>().sqrMagnitude < 0.01f && isSwimming)
        {
            animator.SetBool(isSwimmingHash, false);
        }
    }
}
