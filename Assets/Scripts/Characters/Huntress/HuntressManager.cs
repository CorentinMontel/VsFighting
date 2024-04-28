using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Huntress;
using Characters.Huntress.StateMachine;
using Lib.Character;
using Lib.StateMachine;
using UnityEngine;

public class HuntressManager : MonoBehaviour, IAttackable, IGroundedCharacter
{
    public Animator huntressAnimator;
    public HuntressMovement huntressMovement;
    public CharacterHealth huntressHealth;
    public readonly StateMachine huntressStateMachine = new StateMachine();
    public SpriteRenderer huntressRenderer;
    public Transform[] patrolPoints = new Transform[10];
    public Transform center;
    public Characters.Huntress.SwordHitbox SwordHitbox;

    public float patrolSpeed = 200;

    public bool grounded = false;

    public float shortDistanceRange = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        huntressStateMachine.Initialize(new PatrolState(this, patrolPoints));
    }

    // Update is called once per frame
    void Update()
    {
        huntressStateMachine.Update();
        huntressAnimator.SetFloat(HuntressAnimation.HorizontalMovement, Mathf.Abs(huntressMovement.huntressRigidBody.velocity.x));

        if (huntressMovement.huntressRigidBody.velocity.x > 0.1 && !huntressStateMachine.CurrentState.Is("FlipStuck"))
        {
            //huntressRenderer.flipX = false;
            huntressRenderer.transform.localScale = new Vector3(1, 1, 1);
        }

        if (huntressMovement.huntressRigidBody.velocity.x < -0.1 && !huntressStateMachine.CurrentState.Is("FlipStuck"))
        {
            //huntressRenderer.flipX = true;
            huntressRenderer.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        huntressStateMachine.FixedUpdate();
    }

    public void FollowPlayer(GameObject player)
    {
        huntressStateMachine.TransitionTo(new FollowState(this, player.transform));
    }

    public void TakeAttack(Attack attack)
    {
        huntressHealth.ReduceHealth(attack.Power);
        
        //if (huntressStateMachine.CurrentState.Is())
        huntressStateMachine.TransitionTo(new DamageState(this, attack.Source));
    }

    public void SetGrounded(bool value)
    {
        grounded = value;
    }
}
