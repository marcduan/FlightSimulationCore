using System.Collections;
using System.Collections.Generic;
/*
  
using UnityEngine;


public class AirplaneController : MonoBehaviour
{
    public float speed = 10f;
    public float pitchSpeed = 5f;
    public float yawSpeed = 5f;
    public float rollSpeed = 5f;

    void Update()
    {
        // Throttle control (forward/backward)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Pitch (up/down)
        float pitch = Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime;
        transform.Rotate(pitch, 0, 0);

        // Yaw (left/right)
        float yaw = Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        transform.Rotate(0, yaw, 0);

        // Roll (keyboard inputs for roll, e.g., Q/E)
        if (Input.GetKey(KeyCode.Q)) transform.Rotate(0, 0, rollSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(0, 0, -rollSpeed * Time.deltaTime);
    }
}
*/

/*
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 2.5f;
    public float pitchSpeed = 1f;
    public float yawSpeed = 1f;
    public float rollSpeed = 1f;
    public float liftForce = 9.8f; // Counteracts gravity

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the airplane
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Apply lift to counteract gravity
        rb.AddForce(Vector3.up * liftForce, ForceMode.Force);

        // Move the airplane forward
        rb.AddForce(transform.forward * speed, ForceMode.Force);

        // Pitch (up/down movement)
        float pitch = Input.GetAxis("Vertical") * pitchSpeed;
        rb.AddTorque(transform.right * pitch, ForceMode.Force);

        // Yaw (left/right movement)
        float yaw = Input.GetAxis("Horizontal") * yawSpeed;
        rb.AddTorque(transform.up * yaw, ForceMode.Force);

        // Roll (Q/E keys)
        if (Input.GetKey(KeyCode.Q))
            rb.AddTorque(-transform.forward * rollSpeed, ForceMode.Force);
        if (Input.GetKey(KeyCode.E))
            rb.AddTorque(transform.forward * rollSpeed, ForceMode.Force);
    }
}
*/

/*
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 5f;
    public float pitchSpeed = 2f;
    public float yawSpeed = 2f;
    public float rollSpeed = 2f;
    public float liftForce = 9.8f;  // Counteracts gravity
    public float maxPitch = 30f;    // Max pitch angle
    public float maxYaw = 30f;      // Max yaw angle
    public float maxRoll = 30f;     // Max roll angle




    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the airplane
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Apply lift to counteract gravity
        rb.AddForce(Vector3.up * liftForce, ForceMode.Force);

        // Forward movement (throttle)
        rb.AddForce(transform.forward * speed, ForceMode.Force);

        // Pitch (up/down movement)
        float pitchInput = Input.GetAxis("Vertical");
        float pitch = pitchInput * pitchSpeed;
        rb.AddTorque(transform.right * pitch, ForceMode.Force);

        // Yaw (left/right movement)
        float yawInput = Input.GetAxis("Horizontal");
        float yaw = yawInput * yawSpeed;
        rb.AddTorque(transform.up * yaw, ForceMode.Force);

        // Roll (Q/E keys)
        if (Input.GetKey(KeyCode.Q))
            rb.AddTorque(-transform.forward * rollSpeed, ForceMode.Force);
        if (Input.GetKey(KeyCode.E))
            rb.AddTorque(transform.forward * rollSpeed, ForceMode.Force);

        // Clamp pitch, yaw, and roll to prevent excessive rotation
        LimitRotation();
    }

    // Clamps the rotation to limit the plane's angles
    private void LimitRotation()
    {
        // Limit the rotation around the pitch (x-axis)
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -maxPitch, maxPitch);

        // Limit the rotation around the yaw (y-axis)
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYaw, maxYaw);

        // Limit the rotation around the roll (z-axis)
        currentRotation.z = Mathf.Clamp(currentRotation.z, -maxRoll, maxRoll);

        transform.eulerAngles = currentRotation;
    }
}
*/

/*
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 2f;
    public float pitchSpeed = 0.5f;
    public float yawSpeed = 0.5f;
    public float rollSpeed = 0.5f;
    public float liftForce = 9.8f;  // Counteracts gravity
    public float stabilizationForce = 2f; // Force to stabilize the plane

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Apply lift to counteract gravity
        rb.AddForce(Vector3.up * liftForce, ForceMode.Force);

        // Move forward
        rb.AddForce(transform.forward * speed, ForceMode.Force);

        // Handle user input for pitch, yaw, and roll
        HandleControls();

        // Stabilize the airplane
        StabilizePlane();
    }

    void HandleControls()
    {
        // Pitch (up/down movement)
        float pitchInput = Input.GetAxis("Vertical");
        rb.AddTorque(transform.right * pitchInput * pitchSpeed, ForceMode.Force);

        // Yaw (left/right movement)
        float yawInput = Input.GetAxis("Horizontal");
        rb.AddTorque(transform.up * yawInput * yawSpeed, ForceMode.Force);

        // Roll (Q/E keys)
        if (Input.GetKey(KeyCode.Q))
            rb.AddTorque(-transform.forward * rollSpeed, ForceMode.Force);
        if (Input.GetKey(KeyCode.E))
            rb.AddTorque(transform.forward * rollSpeed, ForceMode.Force);
    }

    void StabilizePlane()
    {
        // Gradually stabilize roll and pitch to avoid uncontrolled spinning
        Vector3 localRotation = transform.localEulerAngles;

        // Normalize angles (e.g., 350° becomes -10°)
        if (localRotation.x > 180) localRotation.x -= 360;
        if (localRotation.z > 180) localRotation.z -= 360;

        // Apply torque to stabilize pitch
        rb.AddTorque(-transform.right * localRotation.x * stabilizationForce, ForceMode.Force);

        // Apply torque to stabilize roll
        rb.AddTorque(-transform.forward * localRotation.z * stabilizationForce, ForceMode.Force);
    }
}
*/

/*
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 3f;          // Forward speed of the plane
    public float pitchSpeed = 2f;      // Control speed for pitch (up/down)
    public float yawSpeed = 2f;        // Control speed for yaw (left/right)
    public float rollSpeed = 2f;       // Control speed for roll (rotation around z-axis)
    public float liftForce = 9.8f;     // Counteract gravity

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the airplane
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Forward movement (throttle)
        rb.AddForce(transform.forward * speed, ForceMode.Force);

        // Control for pitch (up/down)
        float pitchInput = Input.GetAxis("Vertical");
        rb.AddTorque(transform.right * pitchInput * pitchSpeed, ForceMode.Force);

        // Control for yaw (left/right)
        float yawInput = Input.GetAxis("Horizontal");
        rb.AddTorque(transform.up * yawInput * yawSpeed, ForceMode.Force);

        // Control for roll (Q/E keys)
        if (Input.GetKey(KeyCode.Q))
            rb.AddTorque(-transform.forward * rollSpeed, ForceMode.Force);
        if (Input.GetKey(KeyCode.E))
            rb.AddTorque(transform.forward * rollSpeed, ForceMode.Force);

        // Apply lift force to keep the plane in the air
        rb.AddForce(Vector3.up * liftForce, ForceMode.Force);
    }
}
*/


/*
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    // Physics variables
    public float thrust = 50f; // Forward thrust power
    public float pitchSpeed = 5f; // Speed for pitching (up and down)
    public float yawSpeed = 5f; // Speed for yawing (left and right)
    public float rollSpeed = 5f; // Speed for rolling (tilting sideways)
    public float liftMultiplier = 0.1f; // Proportional lift based on speed

    private Rigidbody rb; // Reference to Rigidbody component

    // Flight data
    private float speed;
    private float altitude;
    private float fuelLevel = 100f; // Start with full fuel

    void Start()
    {
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the airplane object!");
        }

        // Initialize flight data
        speed = 0f;
        altitude = transform.position.y;
    }

    void Update()
    {
        HandleInput(); // Process user input
        UpdateFlightData(); // Update speed, altitude, and fuel
    }

    void FixedUpdate()
    {
        ApplyPhysics(); // Apply physics forces
    }

    private void HandleInput()
    {
        float pitch = Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime; // W/S or Up/Down arrows
        float yaw = Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime; // A/D or Left/Right arrows
        float roll = 0f;

        if (Input.GetKey(KeyCode.Q)) // Roll left
            roll = rollSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.E)) // Roll right
            roll = -rollSpeed * Time.deltaTime;

        // Apply rotations
        transform.Rotate(pitch, yaw, roll);
    }

    private void ApplyPhysics()
    {
        if (rb == null) return;

        // Apply forward thrust
        Vector3 thrustForce = transform.forward * thrust;
        rb.AddForce(thrustForce);

        // Simulate lift proportional to forward speed
        float lift = rb.velocity.magnitude * liftMultiplier;
        Vector3 liftForce = Vector3.up * lift;
        rb.AddForce(liftForce);

        // Optional: Apply drag to stabilize the flight
        rb.drag = 0.05f; // Linear drag (reduce if too slow)
        rb.angularDrag = 0.1f; // Rotational drag (reduce for agile movement)
    }

    private void UpdateFlightData()
    {
        speed = rb.velocity.magnitude; // Speed is the magnitude of velocity
        altitude = transform.position.y; // Altitude is the Y position

        // Simulate fuel consumption
        fuelLevel = Mathf.Max(0, fuelLevel - Time.deltaTime * 0.5f); // Reduce fuel gradually
    }

    public float GetCurrentSpeed()
    {
        return speed;
    }

    public float GetCurrentAltitude()
    {
        return altitude;
    }

    public float GetCurrentFuelLevel()
    {
        return fuelLevel;
    }
}
*/

/*
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 5f;
    public float pitchSpeed = 2f;
    public float yawSpeed = 2f;
    public float rollSpeed = 2f;
    public float liftForce = 9.8f;  // Counteracts gravity
    public float maxPitch = 30f;    // Max pitch angle
    public float maxYaw = 30f;      // Max yaw angle
    public float maxRoll = 30f;     // Max roll angle

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.up * liftForce, ForceMode.Force);
        rb.AddForce(transform.forward * speed, ForceMode.Force);

        float pitchInput = Input.GetAxis("Vertical");
        float pitch = pitchInput * pitchSpeed;
        rb.AddTorque(transform.right * pitch, ForceMode.Force);

        float yawInput = Input.GetAxis("Horizontal");
        float yaw = yawInput * yawSpeed;
        rb.AddTorque(transform.up * yaw, ForceMode.Force);

        if (Input.GetKey(KeyCode.Q))
            rb.AddTorque(-transform.forward * rollSpeed, ForceMode.Force);
        if (Input.GetKey(KeyCode.E))
            rb.AddTorque(transform.forward * rollSpeed, ForceMode.Force);

        LimitRotation();
    }

    private void LimitRotation()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -maxPitch, maxPitch);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYaw, maxYaw);
        currentRotation.z = Mathf.Clamp(currentRotation.z, -maxRoll, maxRoll);
        transform.eulerAngles = currentRotation;
    }

    public float GetCurrentSpeed()
    {
        return rb.velocity.magnitude;
    }

    public float GetCurrentAltitude()
    {
        return transform.position.y;
    }

    public float GetCurrentFuelLevel()
    {
        return 100f; // Placeholder
    }
}

*/
/*

using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 5f;
    public float pitchSpeed = 2f;
    public float yawSpeed = 2f;
    public float rollSpeed = 2f;
    public float liftForce = 9.8f;  // Counteracts gravity
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing!");
        }
    }

    void FixedUpdate()
{
    // Adjust lift force to stabilize airplane
    rb.AddForce(Vector3.up * liftForce, ForceMode.Force);

    // Adjust forward speed
    rb.AddForce(transform.forward * speed, ForceMode.Force);

    // Control airplane (pitch, yaw, roll)
    float pitchInput = Input.GetAxis("Vertical");
    rb.AddTorque(transform.right * pitchInput * pitchSpeed, ForceMode.Acceleration);

    float yawInput = Input.GetAxis("Horizontal");
    rb.AddTorque(transform.up * yawInput * yawSpeed, ForceMode.Acceleration);

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
*/


/*
using UnityEngine;


public class AirplaneController : MonoBehaviour
{
    public float speed = 5f;
    public float pitchSpeed = 2f;
    public float yawSpeed = 2f;
    public float rollSpeed = 2f;
    public float liftForce = 9.8f;  // Counteracts gravity
    private Rigidbody rb;

    public float thrust = 5f;  // Default thrust value
    public float weight = 1000f;  // Default weight value

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing!");
        }
    }



    void FixedUpdate()
    {
        rb.mass = weight; // Apply weight to Rigidbody
        

        rb.AddForce(Vector3.up * (liftForce + thrust), ForceMode.Force); // Include thrust in lift
        rb.AddForce(transform.forward * speed, ForceMode.Force);

        float pitchInput = Input.GetAxis("Vertical");
        rb.AddTorque(transform.right * pitchInput * pitchSpeed, ForceMode.Acceleration);

        float yawInput = Input.GetAxis("Horizontal");
        rb.AddTorque(transform.up * yawInput * yawSpeed, ForceMode.Acceleration);

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
*/

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

    //12/28 10.00
    
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
    

    /*
    void FixedUpdate()
    {
        // Ensure thrust and weight are valid
        thrust = Mathf.Max(0, thrust);
        weight = Mathf.Max(1, weight); // Avoid weight of zero

        // Update the Rigidbody mass
        rb.mass = weight;

        // Calculate required lift to hover and add thrust for upward motion
        float requiredLift = weight * Physics.gravity.magnitude; // Lift needed to counteract gravity
        float totalLift = requiredLift + thrust; // Total lift force (gravity + thrust)

        // Apply lift force
        rb.AddForce(Vector3.up * totalLift, ForceMode.Force);

        // Forward movement based on thrust
        rb.AddForce(transform.forward * thrust, ForceMode.Force);

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
    */



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

