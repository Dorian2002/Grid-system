using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public abstract GameObject Obj { get; set; }
    public abstract float Type { get; set; }
}
