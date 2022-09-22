using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditorInternal.VersionControl.ListControl;

public class FloorGeneration : MonoBehaviour
{
    public int size;

    Room[,] rooms;
    List<Vector2Int> notNull = new List<Vector2Int>();
    public int width, height, numberOfRooms;
    public List<Room> roomPrefab;
    bool isFirst = true;
    void Start()
    {
        rooms = new Room[width, height];
        for (int i = 0; i < numberOfRooms; i++)
        {
            for (int cycle = 0; cycle < 20; cycle++)
            {
                if (GenerateRoom())
                {
                    break;
                }
            }
        }
    }

    private bool GenerateRoom()
    {
        Vector2Int pos = new Vector2Int();
        Vector2Int dir = new Vector2Int();
        Room chosenRoom = roomPrefab[Random.Range(0, roomPrefab.Count)];
        if (isFirst)
        {
            pos.x = width / 2;
            pos.y = height / 2;
            isFirst = false;
        }
        else
        {
            dir = RandomDirection();
            int randomNotNull = Random.Range(0, notNull.Count);
            pos = notNull[randomNotNull] + dir;
            if (!(pos.x >= 0 && pos.y >= 0 && pos.x < rooms.GetLength(0) && pos.y < rooms.GetLength(1)))
            {
                return false;
            }
        }
        for (int x = 0; x < chosenRoom.width; x++)
        {
            for (int y = 0; y < chosenRoom.height; y++)
            {
                int posx = 0, posy = 0;
                if (dir == Vector2Int.right || dir == Vector2Int.up)
                {
                    posx = pos.x + x;
                    posy = pos.y + y;
                }
                else if (dir == Vector2Int.left)
                {
                    posx = pos.x - x;
                    posy = pos.y + y;
                }
                else if (dir == Vector2Int.down)
                {
                    posx = pos.x + x;
                    posy = pos.y - y;
                }
                if (posx >= 0 && posy >= 0 && posx < rooms.GetLength(0) && posy < rooms.GetLength(1))
                {
                    if (rooms[posx, posy] != null)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        int instPosx = 0, instPosy = 0, roomPosx = 0, roomPosy = 0;
        if (dir == Vector2Int.right || dir == Vector2Int.up || dir == Vector2Int.zero)
        {
            instPosx = (pos.x - rooms.GetLength(0) / 2) * size;
            instPosy = (pos.y - rooms.GetLength(1) / 2) * size;
            roomPosx = 1;
            roomPosy = 1;
        }
        else if (dir == Vector2Int.left)
        {
            instPosx = (pos.x - chosenRoom.width + 1 - rooms.GetLength(0) / 2) * size;
            instPosy = (pos.y - rooms.GetLength(1) / 2) * size;
            roomPosx = -1;
            roomPosy = 1;
        }
        else if (dir == Vector2Int.down)
        {
            instPosx = (pos.x - rooms.GetLength(0) / 2) * size;
            instPosy = (pos.y - chosenRoom.height + 1 - rooms.GetLength(1) / 2) * size;
            roomPosx = 1;
            roomPosy = -1;
        }

        Room room = GameObject.Instantiate(chosenRoom, new Vector3(instPosx, instPosy, 0) + transform.position, Quaternion.Euler(0, 0, 0), transform);
        if (neighboursCount(room, pos.x, pos.y) > 1)
        {
            GameObject.Destroy(room.gameObject);
            return false;
        }
        for (int x = 0; x < chosenRoom.width; x++)
        {
            for (int y = 0; y < chosenRoom.height; y++)
            {
                rooms[pos.x + x * roomPosx, pos.y + y * roomPosy] = room;
                notNull.Add(new Vector2Int(pos.x + x * roomPosx, pos.y + y * roomPosy));
            }
        }
        return true;
    }

    private Vector2Int RandomDirection()
    {
        List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        return directions[Random.Range(0, directions.Count)];
    }

    private int neighboursCount(Room room, int x, int y)
    {
        int neigh = 0;
        for (int w = 0; w < room.width; w++)
        {
            if (rooms[x + w, y - 1] != null)
            {
                neigh++;
            }
            if (rooms[x + w, y + room.height] != null)
            {
                neigh++;
            }
        }

        for (int h = 0; h < room.height; h++)
        {
            if (rooms[x - 1, y + h] != null)
            {
                neigh++;
            }
            if (rooms[x + room.width, y + h] != null)
            {
                neigh++;
            }
        }

        return neigh;
    }
}
