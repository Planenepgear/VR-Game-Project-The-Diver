using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SwimmingController : MonoBehaviour
{
    public GameObject XROrigin;
    public Transform HeadPos;
    public Transform characterModle;

    //public float SpeedDecay = 0.25f;
    public float SpeedDecayMove = 0.25f;
    public float SpeedDecayRotation = 0.25f;

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    private Rigidbody rb;
    //static float t = 0.0f;

    public Vector3 controllerVelocity;
    //public Quaternion controllerRotation;
    UnityEngine.XR.HapticCapabilities capabilities;

    //[SerializeField] float hapticAmplitude = 1f;
    //[SerializeField] float hapticDuration = 0.1f;

    [SerializeField] float targetSpeed = 0.1f;
    //[SerializeField] float continueTime = 0.2f;

    void Start()
    {
        rb = XROrigin.GetComponent<Rigidbody>();
        TryInitialize();
    }

    void TryInitialize()  // ≥¢ ‘≥ı ºªØ
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + "Swimming controller ready");
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    private void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonPressed);
            targetDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 deviceVelocityValue);
            //targetDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion deviceRotationValue);
            targetDevice.TryGetHapticCapabilities(out capabilities);

            //controllerVelocity = deviceVelocityValue;
            controllerVelocity = Quaternion.AngleAxis(XROrigin.transform.rotation.eulerAngles.y - characterModle.transform.rotation.eulerAngles.y, Vector3.up) * deviceVelocityValue;

            var speed = controllerVelocity.sqrMagnitude;

            if (speed >= targetSpeed)
            {
                //rb.AddRelativeForce(-controllerVelocity * 0.5f);

                if (gripButtonPressed && Mathf.Abs(controllerVelocity.x) > Mathf.Abs(controllerVelocity.y))
                {
                    rb.AddTorque(new Vector3(0, -controllerVelocity.x * 0.8f, 0));
                }
                else if (controllerVelocity.z < 0 && !gripButtonPressed)
                {
                    if (Mathf.Abs(controllerVelocity.z) * 2 > Mathf.Abs(controllerVelocity.x) && Mathf.Abs(controllerVelocity.z) * 2 > Mathf.Abs(controllerVelocity.y))
                    {
                        rb.AddForce(HeadPos.forward * (-controllerVelocity.z + Mathf.Abs(controllerVelocity.x) * 0.3f + Mathf.Abs(controllerVelocity.x) * 0.2f) * 0.7f);
                    }
                }
                else if(gripButtonPressed && Mathf.Abs(controllerVelocity.x) < Mathf.Abs(controllerVelocity.y))
                {
                    rb.AddForce(this.transform.parent.parent.up * -controllerVelocity.y * 0.7f);
                }

                //if(controllerRotation.eulerAngles.y > 90f && controllerRotation.eulerAngles.y <= 120f && controllerVelocity.x < 0)
                //{
                //    rb.AddTorque(new Vector3(0, -controllerVelocity.x * 2f, 0));
                //}

                //if (controllerRotation.eulerAngles.y > 270f && controllerRotation.eulerAngles.y <= 300f && controllerVelocity.x > 0)
                //{
                //    rb.AddTorque(new Vector3(0, -controllerVelocity.x * 2f, 0));
                //}
                //rb.AddTorque(new Vector3(0, -controllerVelocity.x, 0));
            }

            if (rb.velocity.sqrMagnitude > 0)
            {
                rb.AddForce(-rb.velocity * SpeedDecayMove);
            }

            if(rb.angularVelocity.sqrMagnitude > 0)
            {
                rb.AddTorque(-rb.angularVelocity * SpeedDecayRotation);
            }
        }
    }
}
