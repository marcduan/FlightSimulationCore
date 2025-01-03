using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 5f;
    public float pitchSpeed = 2f;
    public float yawSpeed = 2f;
    public float rollSpeed = 2f;
    public float liftForce = 9.8f;  // Base lift force to counteract gravity
    private Rigidbody rb;

    public float thrust = 5f;  // Adjustable thrust
    public float weight = 1000f;  // Adjustable weight

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing!");
        }

        rb.mass = weight; // Set initial mass
    }


    void FixedUpdate()
    {
        // Ensure thrust and speed are valid
        thrust = Mathf.Max(0, thrust);
        speed = Mathf.Max(0, speed);

        // Apply weight and calculate required lift
        rb.mass = weight; // Ensure mass is updated
        float requiredLift = rb.mass * Physics.gravity.magnitude;
        //rb.AddForce(Vector3.up * (requiredLift + thrust), ForceMode.Force); // Adjust lift force dynamically
        rb.AddForce(Vector3.up * requiredLift, ForceMode.Force); // Maintain altitude
        rb.AddForce(transform.forward * thrust, ForceMode.Force); // Use thrust for forward motion


        // Forward movement
        rb.AddForce(transform.forward * speed, ForceMode.Force);

        // Pitch control (nose up/down)
        float pitchInput = Input.GetAxis("Vertical");
        rb.AddTorque(transform.right * pitchInput * pitchSpeed, ForceMode.Acceleration);

        // Yaw control (turn left/right)
        float yawInput = Input.GetAxis("Horizontal");
        rb.AddTorque(transform.up * yawInput * yawSpeed, ForceMode.Acceleration);

        // Roll control (tilt left/right)
        if (Input.GetKey(KeyCode.Q))
            rb.AddTorque(-transform.forward * rollSpeed, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.E))
            rb.AddTorque(transform.forward * rollSpeed, ForceMode.Acceleration);
    }


    public float GetCurrentSpeed()
    {
        return rb.velocity.magnitude; // Get speed from Rigidbody
    }

    public float GetCurrentAltitude()
    {
        return transform.position.y; // Get Y-position as altitude
    }

    public float GetCurrentFuelLevel()
    {
        return Mathf.Max(0, 100 - Time.time * 0.1f); // Decrease fuel over time
    }
}

