using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowGameObject : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _currentVelocity;
    public Vector3 offset = new Vector3(0, 0 , -10);

    public Transform followed;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _camera.transform.position =
            Vector3.SmoothDamp(_camera.transform.position, followed.position + offset, ref _currentVelocity, 0.1f);
    }

    public void SetFollowed(Transform element)
    {
        followed = element;
    }
}
