using UnityEngine;

namespace Lib.Character
{
    public class Attack
    {
        public float Power { get; private set;}
        public Transform Source { get; private set;}
        
        public Attack(float power, Transform source)
        {
            Power = power;
            Source = source;
        }
    }
}