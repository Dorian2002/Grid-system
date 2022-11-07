using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using static UnityEngine.Random;
using UnityEngine;
using UnityEngine.AI;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    private Block[,] MyGrid { get; set; }
    public List<Block> spawns;
    private Transform _spawn;
    private int _lenght;
    private float wallValue;
    private int seed;
    private NavMeshSurface2d _navMesh;

    public Map(Transform transform, int lenght, NavMeshSurface2d navMesh)
    {
        _navMesh = navMesh;
        seed = (int)System.DateTime.Now.Ticks;
        _spawn = transform;
        _lenght = lenght;
        MyGrid = new Block[_lenght,_lenght];
        
        CreateBaseMap();
    }
    
    private void CreateBaseMap()
    {
        float[,] noisemap = GetNoiseMap();
        for (int x = 0; x < _lenght; x++) {
            for (int y = 0; y < _lenght; y++)
            {
                Block obj;
                float noiseValue = noisemap[x, y];
                if (x == 0 || y == 0 || x == _lenght-1 || y == _lenght-1)
                {
                    obj = Resources.Load<Wall>("Prefabs/Wall");
                }
                else if (noiseValue < wallValue)
                {
                    obj = Resources.Load<Wall>("Prefabs/Wall");
                }
                else
                {
                    obj = Resources.Load<Floor>("Prefabs/Floor");
                }
                obj = Instantiate(obj,_spawn);
                obj.transform.position = new Vector2(x, y);
                MyGrid[x,y] = obj;
            }
        }
        CheckForSpawns();
    }

    private float[,] GetNoiseMap()
    {
        spawns = new List<Block>();
        float[,] noisemap = new float[_lenght, _lenght];
        InitState(seed);
        var noise = Range(0f, 100f);
        for (int x = 0; x < _lenght; x++)
        {
            for (int y = 0; y < _lenght; y++)
            {
                //float perlinX = noise + x / _lenght * 1;
                //float perlinY= noise + x / _lenght * 1;
                noisemap[x, y] =  Mathf.PerlinNoise(x * noise / _lenght, y * noise / _lenght);
                wallValue += noisemap[x, y];
            }
        }

        wallValue = ((wallValue / (noisemap.Length))/3*2);
        return noisemap;
    }

    private void CheckForSpawns()
    {
        for (int x = 2; x <= _lenght-2; x++)
        {
            for (int y = 2; y < _lenght-2; y++)
            {
                if (
                    MyGrid[x,y].Type == 0 &&
                    MyGrid[x,y+1].Type == 0 && 
                    MyGrid[x,y-1].Type == 0 &&
                    MyGrid[x+1,y].Type == 0 &&
                    MyGrid[x+1,y+1].Type == 0 && 
                    MyGrid[x+1,y-1].Type == 0 &&
                    MyGrid[x-1,y].Type == 0 &&
                    MyGrid[x-1,y+1].Type == 0 && 
                    MyGrid[x-1,y-1].Type == 0
                )
                {
                    spawns.Add(MyGrid[x,y]);
                }
            }
        }

        if (spawns.Count >= 130)
        {
            //CreateBaseMap();
        }
        InitState(seed);

        int index;
        Block crate;
        for (int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                    index = Range(0, (spawns.Count/2)-1);
                    crate = Instantiate(Resources.Load<Crate>("Prefabs/Crate"), _spawn);
                    crate.transform.position = spawns[index].transform.position;
                    Destroy(spawns[index].gameObject);
                    break;
                case 1:
                    index = Range(spawns.Count/2, spawns.Count-1);
                    crate = Instantiate(Resources.Load<Crate>("Prefabs/Crate"), _spawn);
                    crate.transform.position = spawns[index].transform.position;
                    Destroy(spawns[index].gameObject);
                    break;
                case 2:
                    index = Range(0, (spawns.Count/2)-1);
                    crate = Instantiate(Resources.Load<CratePlate>("Prefabs/CratePlate"), _spawn);
                    crate.transform.position = spawns[index].transform.position;
                    Destroy(spawns[index].gameObject);
                    break;
                case 3:
                    index = Range(spawns.Count/2, spawns.Count-1);
                    crate = Instantiate(Resources.Load<CratePlate>("Prefabs/CratePlate"), _spawn);
                    crate.transform.position = spawns[index].transform.position;
                    Destroy(spawns[index].gameObject);
                    break;
                case 4:
                    index = Range(0, spawns.Count-1);
                    var player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), _spawn);
                    player.transform.position = spawns[index].transform.position;
                    spawns.RemoveAt(index);
                    break;
            }
        }

        _navMesh.BuildNavMesh();
    }
}
