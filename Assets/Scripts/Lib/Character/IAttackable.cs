using UnityEngine;

namespace Lib.Character
{
    public interface IAttackable
    {
        public void TakeAttack(Attack attack);
    }
}