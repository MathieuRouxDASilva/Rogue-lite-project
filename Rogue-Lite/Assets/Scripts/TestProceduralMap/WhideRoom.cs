using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhideRoom : MonoBehaviour
{
    //serializefield variables
    [SerializeField] private GameObject topDoor;
    [SerializeField] private GameObject downDoor;
    [SerializeField] private GameObject leftDoorUpSide;
    [SerializeField] private GameObject rightDoorUpSide;
    [SerializeField] private GameObject leftDoorDownSide;
    [SerializeField] private GameObject rightDoorDownSide;

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
            rightDoorUpSide.SetActive(true);
        }
        if (direction == Vector2Int.left)
        {
            leftDoorUpSide.SetActive(true);
        }
        if (direction == Vector2Int.right * 2)
        {
            rightDoorDownSide.SetActive(true);
        }
        if (direction == Vector2Int.left * 2)
        {
            leftDoorDownSide.SetActive(true);
        }
    }
}
