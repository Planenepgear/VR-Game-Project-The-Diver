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

    private float SpeedDecay = 1;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(LTrigger.action.ReadValue<float>() > 0.1f)
        {
            rb.AddForce(transform.up * LMoveaction.action.ReadValue<Vector2>().sqrMagnitude * 1.25f);
        }
        else if (RTrigger.action.ReadValue<float>() > 0.1f)
        {
            rb.AddForce(-transform.up * LMoveaction.action.ReadValue<Vector2>().sqrMagnitude * 1.25f);
        }

        if (rb.velocity.sqrMagnitude > 0.001f)
        {
            rb.AddForce(-rb.velocity * SpeedDecay);
        }
    }
}
