using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePlate : Block
{
    public override GameObject Obj { get; set; }
    public override float Type { get; set; } = 3;

    private bool used = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("test");
        if (col.CompareTag("Crate") && !used)
        {
            used = true;
            Destroy(col.gameObject);
        }
    }
}
