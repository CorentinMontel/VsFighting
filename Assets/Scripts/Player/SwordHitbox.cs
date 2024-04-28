using System;
using System.Collections;
using System.Collections.Generic;
using Lib.Character;
using UnityEditor.UIElements;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public Collider2D SwordCollider;
    public string colliderTag = "Npc";
    public Attack attack = null;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Npc") && attack != null)
        {
            IAttackable defender = other.GetComponentInParent<IAttackable>();
            
            defender.TakeAttack(attack);
        }
    }

    // Todo : Remove flip to PlayerManager or PlayerMovement, not the right place for that
    public void FlipX(bool flip)
    {
        SwordCollider.transform.localScale = new Vector3(flip ? -1 : 1, 1, 1);
    }
}
