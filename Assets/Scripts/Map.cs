using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using static UnityEngine.Random;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    private float[,] Grid { get; set; }
    private Transform _spawn;
    private int _lenght;

    public Map(Transform transform, int lenght)
    {
        _spawn = transform;
        _lenght = lenght;
        Grid = new float[_lenght,_lenght];
        
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
                else if (noiseValue < 0.35f)
                {
                    obj = Resources.Load<Wall>("Prefabs/Wall");
                }
                else
                {
                    obj = Resources.Load<Floor>("Prefabs/Floor");
                }
                obj = Instantiate(obj,_spawn);
                obj.transform.position = new Vector2(
                    obj.transform.position.x+(x==0?x:(float)x/2), 
                    obj.transform.position.y+(y==0?y:(float)y/2));
                Debug.Log(noiseValue);
                Grid[x,y] = obj.Type;
            }
        }
    }

    private float[,] GetNoiseMap()
    {
        float[,] noisemap = new float[_lenght, _lenght];
        Random.InitState((int)System.DateTime.Now.Ticks);
        var noise = Random.Range(0f, 0.5f);
        for (int x = 0; x < _lenght; x++)
        {
            for (int y = 0; y < _lenght; y++)
            {
                noisemap[x, y] =  Mathf.PerlinNoise(x+noise, y+noise);
            }
        }

        return noisemap;
    }
}
