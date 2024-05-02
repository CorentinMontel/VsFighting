using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetector : MonoBehaviour
{
    public Collider2D Collider;

    private bool used = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Bounds GetBounds()
    {
        return Collider.bounds;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag(""))
    }
}
