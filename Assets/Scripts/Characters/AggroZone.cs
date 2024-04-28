using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AggroZone : MonoBehaviour
{
    public string triggerTag = "default";

    public UnityEvent<GameObject> enterCallback = null;
    public UnityEvent<GameObject> exitCallback = null;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag) && null != enterCallback)
        {
            enterCallback.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag) && null != enterCallback)
        {
            exitCallback.Invoke(other.gameObject);
        }
    }
}
