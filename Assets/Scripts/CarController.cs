using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentAngle;// saða sola kayma açýsýna göre kontrol
    private float currentBrake;// fren kuvveti
    private bool isBrake;

    [SerializeField] private float speed;
    [SerializeField] private float brake;
    [SerializeField] private float maxAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelMesh;
    [SerializeField] private Transform frontRightWheelMesh;
    [SerializeField] private Transform rearLeftWheelMesh;
    [SerializeField] private Transform rearRightWheelMesh;


    private void FixedUpdate()
    {
        GetInput();
        Motor();
        Steering();
        turnWheels();



    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(Horizontal);
        verticalInput = Input.GetAxis(Vertical);
        isBrake = Input.GetKey(KeyCode.Space);
    }

    private void Motor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * speed;
        frontRightWheelCollider.motorTorque = verticalInput * speed;
        currentBrake = isBrake ? brake : 0f;
        Brake();
    }

    private void Brake()
    {
        frontRightWheelCollider.brakeTorque = currentBrake;
        frontLeftWheelCollider.brakeTorque = currentBrake;
        rearLeftWheelCollider.brakeTorque = currentBrake;
        rearRightWheelCollider.brakeTorque = currentBrake;
    }

    private void Steering()
    {
        currentAngle = maxAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentAngle;
        frontRightWheelCollider.steerAngle = currentAngle;
    }

    private void turnWheels()
    {
        TurnWheel(frontLeftWheelCollider, frontLeftWheelMesh);
        TurnWheel(frontRightWheelCollider, frontRightWheelMesh);
        TurnWheel(rearLeftWheelCollider, rearLeftWheelMesh);
        TurnWheel(rearRightWheelCollider, rearLeftWheelMesh);

    }

    private void TurnWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;

    }


}
