using System;
using System.Collections;
using System.Collections.Generic;
using Lib.Character;
using UnityEngine;

public class GroundedDetector : MonoBehaviour
{
    public GameObject objectManager;

    private IGroundedCharacter _groundedManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _groundedManager = objectManager.GetComponent<IGroundedCharacter>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("World"))
        {
            _groundedManager.SetGrounded(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("World"))
        {
            _groundedManager.SetGrounded(false);
        }
    }
}
