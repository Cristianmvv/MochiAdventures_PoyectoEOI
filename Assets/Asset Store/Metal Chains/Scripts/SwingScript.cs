using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingScript : MonoBehaviour
{
    private HingeJoint2D hinge;
    private JointMotor2D motorRef;
    private float speed;
    
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>(); //Sets the variable to the hinge joint component attached to the object
        motorRef = hinge.motor; //We have to create a reference to the motor as we can not change it directly
        speed = Mathf.Abs(motorRef.motorSpeed);
    }
    
    void Update()
    {
        Debug.Log(hinge.jointAngle);
        if(hinge.jointAngle >= hinge.limits.max)
        {
            motorRef.motorSpeed = -speed; //Changes direction of swing once the limits are reached
            hinge.motor = motorRef; //Changes the force of the motor
        }    

        if(hinge.jointAngle <= hinge.limits.min)
        {
            motorRef.motorSpeed = speed;
            hinge.motor = motorRef;
        }
    }
}
