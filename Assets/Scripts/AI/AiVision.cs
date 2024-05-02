using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.AI
{
    public class AiVision : MonoBehaviour
    {
        public Transform Source;
        public int RayCount = 35;
        public float FrameRate = 0.1f;
        public bool DebugMode = false;

        private float _lastTime = 0.0f;

        private void Update()
        {
            if (Time.realtimeSinceStartup - _lastTime > FrameRate)
            {
                Visualize();
                _lastTime = Time.realtimeSinceStartup;
            }
        }

        public void Visualize()
        {
            float step = (2 * Mathf.PI) / RayCount;
            for (float i = 0; i < 2 * Mathf.PI; i += step)
            {
                Vector2 vec = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
                RaycastHit2D[] results = new RaycastHit2D[5];
                int layerMask = ~0;
                var size = Physics2D.RaycastNonAlloc(Source.transform.position, vec, results, Mathf.Infinity, layerMask);
                
                if (DebugMode)
                {
                    for (int j = 0; j < size; j++)
                    {
                        RaycastHit2D hit = results[j];
                        Debug.Log(hit);
                    }
                    Debug.DrawRay(Source.position, vec * GameConfig.EnemyViewDistance, Color.blue, 0.1f);
                }
            }
        }
    }
}