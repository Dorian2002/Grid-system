using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private Map map;
    
    void Awake()
    {
        map = new Map(transform, 20);
        map = null;
    }
}
