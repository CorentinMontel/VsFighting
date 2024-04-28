using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject Character;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set player position on current GO and destroy spawn point
        Character.transform.position = transform.position;
        Destroy(gameObject);
    }
}
