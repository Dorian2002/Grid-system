using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public Transform _transform;
    public Vector3 _position;

    public Move(Transform transform, Vector3 postition)
    {
        _transform = transform;
        _position = postition;
    }
    
}
