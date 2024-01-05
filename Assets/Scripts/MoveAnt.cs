using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnt : MonoBehaviour
{
    public float maxSpeed = 3.5f;
    public float steerStrength = 6f;
    public float wanderStrength = 8f;

    Vector2 desiredDirection;
    Vector2 velocity = Vector2.zero;
    Vector2 position;

    Vector2 currentForwardDir;

    public LayerMask obstacleMask;

    public GameObject target;

    RaycastHit2D checkCollisionInWanderDirection()
    {
        return Physics2D.Raycast(position, desiredDirection, maxSpeed, obstacleMask);

    }
    Vector2 WanderDirection()
    {
        desiredDirection = (desiredDirection + UnityEngine.Random.insideUnitCircle).normalized * wanderStrength;
        if (checkCollisionInWanderDirection()) desiredDirection *= -1;
        return desiredDirection;
    }

    void Update()
    {
        position = transform.position;

        if (target)
            desiredDirection = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
        if (checkCollisionInWanderDirection())
        {
            desiredDirection *= -1;
        }
        else
            desiredDirection = WanderDirection();


        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        currentForwardDir = velocity.normalized;
        transform.SetPositionAndRotation(new Vector3(position.x, position.y, -0.1f), Quaternion.FromToRotation(-Vector3.up, -currentForwardDir));



    }
}