using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;

    public float minGyro = 0.0f;
    //public float SpeedDecayMove = 0.25f;
    public float maxSpeed = 3f;
    public float standardHapticAmplitude = 0.3f;

    public GameObject XROrigin;
    public Transform HeadPos;
    public Transform characterModle;

    private Rigidbody rb;

    void Start()
    {
        rb = XROrigin.GetComponent<Rigidbody>();

        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];

            if (j.GetAccel().sqrMagnitude > minGyro)
            {
                gyro = j.GetGyro();

                if(rb.velocity.sqrMagnitude < maxSpeed)
                {
                    //rb.AddForce(HeadPos.forward * (Mathf.Abs(gyro.y) + Mathf.Abs(gyro.x) * 0.3f + Mathf.Abs(gyro.z) * 0.3f) * 0.7f);
                    rb.AddForce(HeadPos.forward * gyro.sqrMagnitude * 0.3f);
                }

                if(gyro.sqrMagnitude > 1f)
                    j.SetRumble(100, 150, Mathf.Clamp(standardHapticAmplitude * gyro.sqrMagnitude * 0.5f, standardHapticAmplitude, standardHapticAmplitude + 0.3f), 300);
            }
            else
            {
                gyro = Vector3.zero;
            }

            accel = j.GetAccel();
        }

        //if (rb.velocity.sqrMagnitude > 0)
        //{
        //    rb.AddForce(-rb.velocity * SpeedDecayMove);
        //}
    }
}
