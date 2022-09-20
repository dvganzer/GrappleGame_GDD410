using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Bob
{   //ApplyGravity_Variables
        public Vector3 velocity;
        public float gravity = 20f;
        public Vector3 gravityDirection = new Vector3(0, 1, 0);
    //ApplyDampening_Variables
        Vector3 dampeningDirection;
        public float drag;
    //CapMaxSpeed_Variables
        public float maximumSpeed;
    public void ApplyGravity()
    {
        velocity -= gravityDirection * gravity * Time.deltaTime;
    }

    public void ApplyDampening()
    {
        dampeningDirection = -velocity;
        dampeningDirection *= drag;
        velocity += dampeningDirection;
    }

    public void CapMaxSpeed()
    {
        velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);
    }
}

