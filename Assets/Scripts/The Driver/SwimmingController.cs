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
    [SerializeField] float standardHapticAmplitude = 0.1f;
    [SerializeField] float hapticDuration = 0.2f;

    [SerializeField] float targetSpeed = 0.1f;
    //[SerializeField] float continueTime = 0.2f;

    void Start()
    {
        rb = XROrigin.GetComponent<Rigidbody>();
        TryInitialize();
    }

    void TryInitialize()  // ≥¢ ‘≥ı ºªØ
    {
        List<InputDevice> devices = new();

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

                if (gripButtonPressed && Mathf.Abs(controllerVelocity.x) > Mathf.Abs(controllerVelocity.y) && Mathf.Abs(controllerVelocity.x) > Mathf.Abs(controllerVelocity.z))
                {
                    rb.AddTorque(new Vector3(0, -controllerVelocity.x * 0.7f, 0));
                    Haptic(speed + 0.5f);
                }
                else if (controllerVelocity.z < 0 && !gripButtonPressed)
                {
                    if (Mathf.Abs(controllerVelocity.z) * 2 > Mathf.Abs(controllerVelocity.x) && Mathf.Abs(controllerVelocity.z) * 2 > Mathf.Abs(controllerVelocity.y))
                    {
                        rb.AddForce((-controllerVelocity.z + Mathf.Abs(controllerVelocity.x) * 0.3f + Mathf.Abs(controllerVelocity.x) * 0.2f) * 0.7f * HeadPos.forward);
                        Haptic(speed);
                    }
                }
                else if (controllerVelocity.z > 0 && Mathf.Abs(controllerVelocity.z) > Mathf.Abs(controllerVelocity.y) && Mathf.Abs(controllerVelocity.z) > Mathf.Abs(controllerVelocity.x) && gripButtonPressed)
                {
                    rb.AddForce(-controllerVelocity.z * 1.7f * HeadPos.forward);
                    Haptic(speed);
                }
                else if(Mathf.Abs(controllerVelocity.y) > 0 && !gripButtonPressed)
                {
                    if(Mathf.Abs(controllerVelocity.y) > Mathf.Abs(controllerVelocity.z))
                    {
                        rb.AddForce((-controllerVelocity.z * 0.3f + Mathf.Abs(controllerVelocity.x) * 0.5f + Mathf.Abs(controllerVelocity.x) * 0.2f) * 0.8f * HeadPos.forward);
                        Haptic(speed);
                    }
                }
                else if(gripButtonPressed && Mathf.Abs(controllerVelocity.x) < Mathf.Abs(controllerVelocity.y))
                {
                    rb.AddForce(0.5f * -controllerVelocity.y * transform.parent.parent.up);
                    Haptic(speed + 0.5f);
                }

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

    public void Haptic(float speed)
    {
        if (speed >= 1.2f)
        {
            uint channel = 0;
            targetDevice.SendHapticImpulse(channel, standardHapticAmplitude, hapticDuration);
            //targetDevice.SendHapticImpulse(channel, Mathf.Clamp(standardHapticAmplitude * speed, standardHapticAmplitude, standardHapticAmplitude + 0.2f), hapticDuration);
        }
    }
}
