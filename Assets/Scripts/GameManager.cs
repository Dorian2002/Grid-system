using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private Map map;
    public List<Block> spawns;
    
    void Awake()
    {
        map = new Map(transform, 30, GetComponent<NavMeshSurface2d>());
        spawns = map.spawns;
        map = null;
    }
}
