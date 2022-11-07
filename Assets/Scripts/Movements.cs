using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Movements : MonoBehaviour
{
    private int speed = 5;
    private void Update()
    {
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(new Vector3(x,y,0));
    }
}
