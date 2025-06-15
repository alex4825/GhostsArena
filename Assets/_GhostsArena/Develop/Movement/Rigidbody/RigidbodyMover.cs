using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMover
{
    private Rigidbody _rigidbody;

    public RigidbodyMover(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Set(Vector3 velocity)
        => _rigidbody.velocity = velocity;
    
    public Vector3 GetVelocity()
        => _rigidbody.velocity;

    public void Update()
    {

    }
}
