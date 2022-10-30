using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Block
{
    [SerializeField] public override GameObject Obj { get; set; }
    public override float Type { get; set; } = 1;
}
