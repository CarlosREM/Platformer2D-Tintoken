﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsObject2D : MonoBehaviour
{
    public float gravModifier = 1f;
    public float minGroundNormalY = 0.65f;

    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected Vector2 targetVelocity;

    protected bool grounded;
    protected Vector2 groundNormal;

    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void OnEnable() {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Start() 
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    
    void Update() 
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    
    protected virtual void ComputeVelocity() {}


    void FixedUpdate()
    {
        velocity += Physics2D.gravity * gravModifier * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPos = velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        // First, calculate horizontal movement

        Vector2 move = moveAlongGround * deltaPos.x;
        Movement(move, false);

        // Then, vertical movement

        move = Vector2.up * deltaPos.y;
        Movement(move, true);
    }


    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance) {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
                hitBufferList.Add(hitBuffer[i]);
            

            for (int i = 0; i < hitBufferList.Count; i++) 
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                    velocity = velocity - projection * currentNormal;
                
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = (modifiedDistance < distance) ? modifiedDistance : distance;
            }
        }

        rb2d.position += move.normalized * distance;
    }

}
