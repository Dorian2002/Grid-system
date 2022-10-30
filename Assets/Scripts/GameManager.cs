using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Map map;
    void Start()
    {
        map = new Map(transform, 19);
    }
}
