using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //serializefield variables
    [SerializeField] private GameObject topDoor;
    [SerializeField] private GameObject downDoor;
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    public Vector2Int roomIndex = new Vector2Int();

    public void ActivateDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
        }
        if (direction == Vector2Int.down)
        {
            downDoor.SetActive(true);
        }
        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
        }
        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
        }
    }
}
