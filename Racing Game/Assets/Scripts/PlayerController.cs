using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //these are the variables that store the player input
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle; //this is the variable that holds the steering angle of the player car

    //these are refrences to the wheel collider component as well as its transform for each individual wheel
    public WheelCollider wc_frontLeftWheel, wc_frontRightWheel, wc_backLeftWheel, wc_backRightWheel;
    public Transform t_frontLeftWheel, t_frontRightWheel, t_backLeftWheel, t_backRightWheel;
    public float maxSteeringAngle = 30.0f; //this is the variable that will control how much we can steer using the steering angle variable
    public float motorForce = 50.0f; //this determines how fastr the player goes and it modifies the motor torque variable in the wheel collider component

    public void GetInput(){ //this just gets the input of our player
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer(){ //this applies the turn radius to the player car
        m_steeringAngle = maxSteeringAngle * m_horizontalInput;

        wc_frontLeftWheel.steerAngle = m_steeringAngle;
        wc_frontRightWheel.steerAngle = m_steeringAngle;
    }

    private void Accelerate(){ //this adds the acceleration to the player car
        wc_backLeftWheel.motorTorque = m_verticalInput * motorForce;
        wc_backRightWheel.motorTorque = m_verticalInput * motorForce;

    }

    private void UpdateWheelPoses(){
        UpdateWheelPose(wc_frontLeftWheel, t_frontLeftWheel);
        UpdateWheelPose(wc_frontRightWheel, t_frontRightWheel);
        UpdateWheelPose(wc_backLeftWheel, t_backLeftWheel);
        UpdateWheelPose(wc_backRightWheel, t_backRightWheel);

    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform){
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate(){
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

}
