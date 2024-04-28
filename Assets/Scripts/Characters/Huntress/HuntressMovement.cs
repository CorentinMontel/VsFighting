using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntressMovement : MonoBehaviour
{
    public Rigidbody2D huntressRigidBody;
    public Collider2D huntressCollider;
    
    private Vector3 velocity = Vector3.zero;

    
    // Start is called before the first frame update
    void Start()
    {
        huntressCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyMovement(Vector2 movement)
    {
        Vector2 huntressVelocity = huntressRigidBody.velocity;
        Vector2 targetVelocity = new Vector2(movement.x * Time.fixedDeltaTime, huntressVelocity.y);
        huntressRigidBody.velocity =
            Vector3.SmoothDamp(huntressVelocity, targetVelocity, ref velocity, 0.05f);
    }

    public void ComputeMovement(Vector2 target)
    {
        //
    }
}
