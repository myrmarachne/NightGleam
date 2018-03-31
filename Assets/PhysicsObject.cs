using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float gravityFactor = 1f;
    public float minimumGroundNormalY = 0.65f;

    protected Rigidbody2D rbody;
        
    protected Vector2 targetVelocity; // Incoming input from outside
    protected Vector2 velocity; // Current velocity of the object
    protected bool grounded; // Flag indicating whether the object is currently staying on the ground
    protected Vector2 groundNormal; // The normal vector to the ground

    protected const float minimumMoveDistance = 0.001f; 
    protected const float shellRadius = 0.01f; // Padding added to collider objects

    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected ContactFilter2D contactFilter;

    private void OnEnable() {
        rbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start () {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
	}
	
	// Update is called once per frame
	void Update () {

        targetVelocity = Vector2.zero;
        ComputeVelocity();
	}

    protected virtual void ComputeVelocity() { }

    private void FixedUpdate() {

        /* Move down the object because of the gravity
         * v = g*t + v_{initial}.
         * Defaultly the below function adds only the
         * vertical velocity change due to gravity */

        velocity = velocity + gravityFactor * Physics2D.gravity * Time.deltaTime;

        // Add the horinzontal velocity from the incoming input
        velocity.x = targetVelocity.x;

        // The position change of the object
        Vector2 deltaPosition = velocity * Time.deltaTime;
 
        grounded = false;

        /* Vector2 moveAlongGround - vector storing the direction we are trying to 
         * move along the ground, which is perpendicular to the ground normal vector */
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        // Horizontal movement (set yMovement flag to false)
        Vector2 horizontalMovement = moveAlongGround * deltaPosition.x;
        Movement(horizontalMovement, false);

        // Vertical movement (set the yMovement flag to true)
        Vector2 verticalMovement = Vector2.up * deltaPosition.y;
        Movement(verticalMovement, true);

    }

    private void Movement(Vector2 movement, bool yMovement) {

        // Get the length of the movement vector
        float distance = movement.magnitude;

        // Check for collisions only if the distance is bigger than the minimum
        // value, in order to avoid constantly checking while standing still
        if (distance > minimumMoveDistance) {

            // Check if any of the attached colliders of the rigidbody will
            // overlap with anything in the next frame
            
            // shellRadius is used to prevent the object from getting stuck in a different collider
            int count = rbody.Cast(movement, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();
            for (int i = 0; i < count; i++) {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++) {
                Vector2 currentNormal = hitBufferList[i].normal;

                // Check if the angle of the object that we are going to collide with
                // means that we can actually "stand" on it - this would not allow
                // sliding from a slope
                if (currentNormal.y > minimumGroundNormalY) {
                    grounded = true;
                    if (yMovement) {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0) {
                    // Cancel the part of the velocity that would be stopped by collision
                    // Example - collision with ceiling
                       velocity = velocity - projection * currentNormal;
                }

                // Scale the distance the rigibody is moved in order to prevent  it
                // from getting stuck in the collider - add padding to the collider
                // (shellRadius) 
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

        }

        // Add movement to the current position of rigidbody, scaled properly
        rbody.position = rbody.position + movement.normalized * distance;
    }
}
