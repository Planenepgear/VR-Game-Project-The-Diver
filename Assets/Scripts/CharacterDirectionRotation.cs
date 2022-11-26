using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CharacterDirectionRotation : MonoBehaviour
{
    public Transform towards;
    public Transform characterModle;
    public InputActionProperty LMoveaction;

    private Vector3 tempTowards;
    private Rigidbody rb;

    public float turnSpeed = 5;

    //public InputActionProperty LJoystick;
    //public InputActionProperty RJoystick;
    //private Vector2 joystickValue;

    private CharacterController characterController;

    private void Start()
    {
        characterController = transform.parent.GetComponent<CharacterController>();
        rb = this.transform.parent.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(LMoveaction.action.ReadValue<Vector2>().sqrMagnitude > 0.1f)
        {
            //joystickValue = LJoystick.action.ReadValue<Vector2>();

            //tempTowards = towards.forward + new Vector3(-joystickValue.x, 0, -joystickValue.y);
            tempTowards = towards.forward + new Vector3(characterController.velocity.x, rb.velocity.y, characterController.velocity.z);

            Quaternion q = Quaternion.LookRotation(tempTowards);
            //Quaternion q = Quaternion.LookRotation(transform.parent.GetComponent<CharacterController>().velocity);
            characterModle.rotation = Quaternion.Slerp(characterModle.rotation, q, turnSpeed * Time.deltaTime);
        }
    }
}
