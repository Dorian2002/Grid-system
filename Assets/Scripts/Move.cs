using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public Transform _transform;
    public Vector2 _position;

    public Move(Transform transform, Vector2 postition)
    {
        _transform = transform;
        _position = postition;
    }
    
}
