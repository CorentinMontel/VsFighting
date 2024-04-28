using System;
using Lib.Character;
using UnityEngine;

namespace Characters.Huntress
{
    public class SwordHitbox : MonoBehaviour
    {
        public Collider2D SwordCollider;

        public Attack attack = null;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && attack != null)
            {
                IAttackable defender = other.GetComponentInParent<IAttackable>();
                // Todo : Better way to do this
                defender.TakeAttack(attack);
            }
        }

        public void FlipX(bool flip)
        {
            SwordCollider.transform.localScale = new Vector3(flip ? -1 : 1, 1, 1);
        }
    }
}
