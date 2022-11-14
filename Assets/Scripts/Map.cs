using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using static UnityEngine.Random;
using UnityEngine;
using UnityEngine.AI;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    private Block[,] MyGrid;
    private List<Block> spawns;
    private Transform _spawn;
    private int _lenght;
    private float wallValue;
    private int seed;
    private List<CratePlate> plates;
    private bool success;
        
    public Map(Transform transform, int lenght)
    {
        seed = (int)System.DateTime.Now.Ticks;
        _spawn = transform;
        _lenght = lenght;
        MyGrid = new Block[_lenght,_lenght];

        success = CreateBaseMap();
    }
    
    private bool CreateBaseMap()
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
        return CheckForSpawns();
    }

    private float[,] GetNoiseMap()
    {
        spawns = new List<Block>();
        float[,] noisemap = new float[_lenght, _lenght];
        InitState(seed);
        var noise = Range(20f, 40f);
        for (int x = 0; x < _lenght; x++)
        {
            for (int y = 0; y < _lenght; y++)
            {
                noisemap[x, y] =  Mathf.PerlinNoise(x * noise / _lenght, y * noise / _lenght);
                wallValue += noisemap[x, y];
            }
        }

        wallValue = ((wallValue / (noisemap.Length))/3*2);
        return noisemap;
    }

    private bool CheckForSpawns()
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

        if (spawns.Count >= 80)
        {
            return false;
        }
        InitState(seed);

        int index;
        Block crate;
        int i = 0;
        List<Vector3> selectedSpawns = new List<Vector3>();
        plates = new List<CratePlate>();
        while (i <5)
        {
            switch (i)
            {
                case 0:
                    index = Range(0, spawns.Count-1);
                    if (selectedSpawns.Contains(spawns[index].transform.position))
                    {
                        continue;
                    }
                    else
                    {
                        crate = Instantiate(Resources.Load<Crate>("Prefabs/Crate"), _spawn);
                        Vector3 cratePosition = crate.transform.position;
                        cratePosition = spawns[index].transform.position;
                        cratePosition = new Vector3(cratePosition.x, cratePosition.y, cratePosition.z);
                        crate.transform.position = cratePosition;
                        selectedSpawns.Add(spawns[index].transform.position);
                        i++;
                        break;
                    }
                case 1:
                    index = Range(0, spawns.Count-1);
                    if (selectedSpawns.Contains(spawns[index].transform.position))
                    {
                        continue;
                    }
                    else
                    {
                        crate = Instantiate(Resources.Load<Crate>("Prefabs/Crate"), _spawn);
                        Vector3 cratePosition = crate.transform.position;
                        cratePosition = spawns[index].transform.position;
                        cratePosition = new Vector3(cratePosition.x, cratePosition.y, cratePosition.z);
                        crate.transform.position = cratePosition;
                        selectedSpawns.Add(spawns[index].transform.position);
                        i++;
                        break;
                    }
                case 2:
                    index = Range(0, spawns.Count-1);
                    if (selectedSpawns.Contains(spawns[index].transform.position))
                    {
                        continue;
                    }
                    else
                    {
                        crate = Instantiate(Resources.Load<CratePlate>("Prefabs/CratePlate"), _spawn);
                        plates.Add((CratePlate)crate);
                        crate.transform.position = spawns[index].transform.position;
                        selectedSpawns.Add(spawns[index].transform.position);
                        Destroy(spawns[index].gameObject);
                        i++;
                        break;
                    }
                case 3:
                    index = Range(0, spawns.Count-1);
                    if (selectedSpawns.Contains(spawns[index].transform.position))
                    {
                        continue;
                    }
                    else
                    {
                        crate = Instantiate(Resources.Load<CratePlate>("Prefabs/CratePlate"), _spawn);
                        plates.Add((CratePlate)crate);
                        crate.transform.position = spawns[index].transform.position;
                        selectedSpawns.Add(spawns[index].transform.position);
                        Destroy(spawns[index].gameObject);
                        i++;
                        break;
                    }
                case 4:
                    index = Range(0, spawns.Count-1);
                    if (selectedSpawns.Contains(spawns[index].transform.position))
                    {
                        continue;
                    }
                    else
                    {
                        var player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), _spawn);
                        Vector3 playerPositon = player.transform.position;
                        playerPositon = spawns[index].transform.position;
                        playerPositon = new Vector3(playerPositon.x, playerPositon.y, playerPositon.z);
                        player.transform.position = playerPositon;
                        
                        player.GetComponent<Player>().SetMyGrid(MyGrid);
                        
                        selectedSpawns.Add(spawns[index].transform.position);
                        i++;
                        break;
                    }
            }
        }
        return true;
    }

    public List<CratePlate> GetPlates()
    {
        return plates;
    }

    public bool GetSuccess()
    {
        return success;
    }
}
