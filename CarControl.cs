using UnityEngine;

public class CarControl : MonoBehaviour
{
    [Header("Car Properties")]
    public float speed = 20.0f;
    public float turnSpeed = 45.0f;
    public float acceleration = 5.0f;
    public float deceleration = 8.0f;
    public float steeringRangeAtMaxSpeed = 30.0f;
    
    // For wheel visual rotation
    public GameObject[] wheelMeshes;
    public float wheelRotationSpeed = 360.0f;
    
    private float currentSpeed = 0.0f;
    private float currentRotation = 0.0f;
    
    void Update()
    {
        // Get input from traditional Input system
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // Handle acceleration and deceleration
        if (verticalInput != 0)
        {
            // Accelerate
            currentSpeed += verticalInput * acceleration * Time.deltaTime;
        }
        else
        {
            // Decelerate when no input
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }
        
        // Clamp speed
        currentSpeed = Mathf.Clamp(currentSpeed, -speed/2, speed);
        
        // Calculate turn amount based on speed
        float speedFactor = Mathf.Abs(currentSpeed) / speed;
        float turnAmount = horizontalInput * Mathf.Lerp(turnSpeed, steeringRangeAtMaxSpeed, speedFactor);
        
        // Only turn if we're moving
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            // Apply rotation
            transform.Rotate(Vector3.up, turnAmount * Time.deltaTime);
        }
        
        // Move the car forward/backward
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        
        // Rotate wheel meshes for visual effect
        if (wheelMeshes != null && wheelMeshes.Length > 0)
        {
            float wheelRotation = currentSpeed * wheelRotationSpeed * Time.deltaTime;
            foreach (GameObject wheel in wheelMeshes)
            {
                wheel.transform.Rotate(wheelRotation, 0, 0);
            }
        }
    }
    
    // Optional: Visual display of current speed
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Speed: " + currentSpeed.ToString("F1"));
    }
}
