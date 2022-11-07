using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int _lenght;
    private Transform _spawn;
    private GameObject[] Rooms;
    private Vector2[] RoomSpawnPoints;
    private List<Vector2> Grid;
    private int direction;
    private int nextDirection;
    public MapGenerator(Transform transform, int lenght)
    {
        Grid = new List<Vector2>();
        Rooms = new GameObject[]
        {
            Resources.Load("Prefabs/RoomLR") as GameObject,
            Resources.Load("Prefabs/RoomLRB") as GameObject,
            Resources.Load("Prefabs/RoomLRT") as GameObject,
            Resources.Load("Prefabs/RoomLRBT") as GameObject,
        };
        _spawn = transform;
        _lenght = lenght;
        
        GameObject obj = Instantiate(Rooms[1],_spawn);
        obj.transform.position = new Vector2(0,0);
        Grid.Add(new Vector2(0,0));

        direction = Random.Range(1, 6);

        for (int i = 1; i < lenght * lenght; i++)
        {
            Move(i);
        }
        //CreateBaseMap();
    }

    private void Move(int i)
    {
        nextDirection = Random.Range(1, 6);
        Vector2 newPos;
        if (direction == 1 || direction == 2)
        {
            newPos = new Vector2(Grid[i].x + 10, Grid[i].y);
            Grid.Add(newPos);
        }

        direction = nextDirection;
    }

    private void CreateBaseMap()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        for (int x = 0; x < _lenght*10; x+=10) {
            for (int y = 0; y < _lenght*10; y+=10)
            {
                int roomNbr = Random.Range(0, Rooms.Length);
                GameObject obj = Instantiate(Rooms[roomNbr],_spawn);
                obj.transform.position = new Vector2(x, y);
            }
        }
    }
}
