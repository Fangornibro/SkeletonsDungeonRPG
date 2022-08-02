using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    public List<GameObject> generationPoints;
    public List<GameObject> roomPrefabs;
    private GameObject newRoom;
    void Start()
    {
        int selectedRoom, selectedPoint;
        GameObject selectedRoomObj;
        while (generationPoints.Count != 0)
        {
            selectedPoint = Random.Range(0, generationPoints.Count);
        M1:
            selectedRoom = Random.Range(0, roomPrefabs.Count);
            if (roomPrefabs[selectedRoom] == null)
            {
                goto M1;
            }
            selectedRoomObj = roomPrefabs[selectedRoom];
            if (roomPrefabs[selectedRoom].GetComponent<RoomPart>().isOnlyOne)
            {
                roomPrefabs[selectedRoom] = null;
            }
            newRoom = Instantiate(selectedRoomObj.GetComponent<RoomPart>().sides[generationPoints[selectedPoint].GetComponent<RoomPartRotation>().getSide()], generationPoints[selectedPoint].transform.position, Quaternion.Euler(0, 0, 0));
            newRoom.transform.SetParent(transform);
            generationPoints[selectedPoint].GetComponentInChildren<Transform>().gameObject.SetActive(false);
            generationPoints.RemoveAt(selectedPoint);
        }
    }
}
