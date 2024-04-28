using System;
using System.Collections;
using System.Collections.Generic;
using Lib.Character;
using Lib.StateMachine;
using Player;
using Player.StateMachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGroundedCharacter, IAttackable
{
    public PlayerMovement PlayerMovement;
    public Animator PlayerAnimator;
    public UiManager UiManager;
    public PlayerInput PlayerInput = new PlayerInput();

    public float maxHealth = 100;
    public float currentHealth;

    public float maxEndurance = 100;
    public int maxJumps = 2;
    public float currentEndurance;
    public float enduranceGain = 15;
    public float enduranceRegenThreshold = 40;
    public bool isEnduranceRegen = false;
    public bool frozen = false;

    public StateMachine StateMachine;
    // Start is called before the first frame update
    void Start()
    {
        SetHealth(maxHealth);
        SetEndurance(maxEndurance);
        
        StateMachine = new StateMachine();
        StateMachine.Initialize(new IdleState(this));
    }
    
    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();
        
        if (isEnduranceRegen && currentEndurance > enduranceRegenThreshold)
        {
            isEnduranceRegen = false;
            UiManager.EnduranceBarManager.SetRegenState(false);
        }
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
        if (currentEndurance < maxEndurance)
        {
            SetEndurance(currentEndurance + (enduranceGain * Time.fixedDeltaTime));
        }
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        UiManager.HealthBarManager.UpdateHealth(health, maxHealth);
    }

    public void ReduceHealth(float amount)
    {
        currentHealth = amount > currentHealth ? 0 : currentHealth - amount;
        UiManager.HealthBarManager.UpdateHealth(currentHealth, maxHealth);
    }

    public void SetEndurance(float amount)
    {
        currentEndurance = amount;
        UiManager.EnduranceBarManager.SetEndurance(amount);
    }

    public bool ConsumeEndurance(float amount)
    {
        if (isEnduranceRegen)
        {
            return false;
        }
        SetEndurance(Mathf.Max(currentEndurance - amount, 0f));
        if (currentEndurance == 0f)
        {
            isEnduranceRegen = true;
            UiManager.EnduranceBarManager.SetRegenState(true);
        }
        return true;
    }

    public void SetGrounded(bool grounded)
    {
        PlayerMovement.grounded = grounded;
    }

    public void TakeAttack(Attack attack)
    {
        ReduceHealth(attack.Power);
        StateMachine.TransitionTo(new DamageState(this, attack.Source));
    }

    public void FreezePlayer()
    {
        frozen = true;
        PlayerAnimator.SetBool(PlayerAnimation.Frozen, true);
    }

    public void UnfreezePlayer()
    {
        frozen = false;
        PlayerAnimator.SetBool(PlayerAnimation.Frozen, false);
    }
}
