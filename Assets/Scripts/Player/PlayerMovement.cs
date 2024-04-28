using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    public float jumpForce = 10;
    
    public Rigidbody2D PlayerRigidbody2D;
    public SpriteRenderer PlayerSpriteRenderer;
    public Animator PlayerAnimator;
    public SwordHitbox SwordHitbox;
    public PlayerManager PlayerManager;
    public Collider2D PlayerCollider;

    public float horizontalMovement = 0f;
    public Vector3 velocity = Vector3.zero;
    public bool jumpRequired = false;
    public bool slideRequired = false;
    public bool grounded = true;
    public bool simpleAttackRequired = false;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput playerInput = PlayerManager.PlayerInput;
        playerInput.Update();
        
        // Compute horizontal movement
        horizontalMovement = playerInput.GetHorizontalAxis() * moveSpeed;

        jumpRequired = playerInput.IsJumping();
        slideRequired = playerInput.IsSliding();
        simpleAttackRequired = playerInput.IsSimpleAttack();

        float xVelocity = PlayerRigidbody2D.velocity.x;
        if (xVelocity > 0.05f)
        {
            //PlayerSpriteRenderer.flipX = false;
            SwordHitbox.FlipX(false);
        }
        if (xVelocity < -0.05f)
        {
            //PlayerSpriteRenderer.flipX = true;
            SwordHitbox.FlipX(true);
        }

        PlayerAnimator.SetBool(PlayerAnimation.Grounded, grounded);
        PlayerAnimator.SetFloat(PlayerAnimation.FallVelocity, PlayerRigidbody2D.velocity.y);
    }

    public void ApplyHorizontalVelocity()
    {
        Vector2 targetVelocity = new Vector2(horizontalMovement * Time.fixedDeltaTime, PlayerRigidbody2D.velocity.y);
        PlayerRigidbody2D.velocity =
            Vector3.SmoothDamp(PlayerRigidbody2D.velocity, targetVelocity, ref velocity, 0.05f);
    }

    public bool CanJump()
    {
        return jumpRequired && !PlayerManager.isEnduranceRegen;
    }

    public void ApplyJump()
    {
        if (PlayerManager.ConsumeEndurance(0))
        {
            PlayerRigidbody2D.velocity = new Vector2(PlayerRigidbody2D.velocity.x, 0);
            PlayerRigidbody2D.AddForce(Vector2.up * (jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            jumpRequired = false;
        }
    }
}
