using System;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class RoomManager : MonoBehaviour
{
    //serializefield variables
    [Header("All rooms")] [SerializeField] private GameObject roomFirstVersion;
    [SerializeField] private GameObject roomSecoundVersion;
    [SerializeField] private GameObject roomThirdVersion;
    [SerializeField] private GameObject roomFourthVersion;
    [SerializeField] private GameObject spawnRoom;
    [SerializeField] private GameObject lootRoom;

    [Header("grid data")] [SerializeField] private int maxRoomsCount = 30;
    [SerializeField] private int minRoomsCount = 20;
    [SerializeField] private int gridSizeX = 10;
    [SerializeField] private int gridSizeY = 10;


    //private int
    private int _roomWith = 20;
    private int _roomHeight = 18;
    private int[,] _roomGrid;
    private int _roomCount;
    private int _numberOfIteration = 0;
    private List<GameObject> _roomsObjects = new List<GameObject>();
    private Queue<Vector2Int> _roomQueue = new Queue<Vector2Int>();
    private bool _generationComplete = false;
    private bool _isShopSpawned = false;

    private bool _isShopUp;
    private bool _isShopDown;
    private bool _isShopRight;


    private void Start()
    {
        //setup
        _roomGrid = new int[gridSizeX, gridSizeY];
        _roomQueue = new Queue<Vector2Int>();

        //create first room in the middle of the grid
        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartWorldGenerationFroomRoom(initialRoomIndex);
    }

    private void StartWorldGenerationFroomRoom(Vector2Int roomIndex)
    {
        //start and instantiate first room, place it and count it
        _roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        _roomGrid[x, y] = 1; //set the room as a actual room (1) not nothing (0)
        _roomCount++;
        //instantiate room and all settings
        var initialRoom = Instantiate(spawnRoom, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{_roomCount}"; //$ + "{roomcount to use it}"
        initialRoom.GetComponent<Room>().roomIndex = roomIndex; //give it the right index
        _roomsObjects.Add(initialRoom);
    }

    private void Update()
    {
        //if system is started -> try to build more rooms
        if (_roomQueue.Count > 0 && _roomCount < maxRoomsCount && !_generationComplete)
        {
            Vector2Int roomIndex = _roomQueue.Dequeue();

            //try generate rooms around actual room
            TryGenerateRoom(new Vector2Int(roomIndex.x - 1, roomIndex.y));
            TryGenerateRoom(new Vector2Int(roomIndex.x + 1, roomIndex.y));
            TryGenerateRoom(new Vector2Int(roomIndex.x, roomIndex.y - 1));
            TryGenerateRoom(new Vector2Int(roomIndex.x, roomIndex.y + 1));
        }
        else if (_roomCount < minRoomsCount)
        {
            //if not enought -> regenerate
            RegenerateRooms();
            Debug.Log("not enought rooms -> regeneration");
        }
        else if (!_generationComplete)
        {
            _generationComplete = true;
            Vector2Int roomIndex = _roomQueue.Dequeue();
            Debug.Log($"Generation complete {_roomCount} number of rooms created");
        }
    }

    //only usefull for the editor (does the same as the update)
    public void Generate()
    {
        if (_roomQueue.Count > 0 && _roomCount < maxRoomsCount && !_generationComplete)
        {
            Vector2Int roomIndex = _roomQueue.Dequeue();

            //try generate rooms around actual room
            TryGenerateRoom(new Vector2Int(roomIndex.x - 1, roomIndex.y));
            TryGenerateRoom(new Vector2Int(roomIndex.x + 1, roomIndex.y));
            TryGenerateRoom(new Vector2Int(roomIndex.x, roomIndex.y - 1));
            TryGenerateRoom(new Vector2Int(roomIndex.x, roomIndex.y + 1));
        }
        else if (_roomCount < minRoomsCount)
        {
            //if not enought -> regenerate
            RegenerateRooms();
            Debug.Log("not enought rooms -> regeneration");
        }
        else if (!_generationComplete)
        {
            _generationComplete = true;
            Debug.Log($"Generation complete {_roomCount} number of rooms created");
        }
    }

    //check all senario when we want the system to stop
    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        if (_roomCount >= maxRoomsCount - 1)
        {
            return false;
        }

        if (Random.value < 0.5f && roomIndex != Vector2Int.zero)
        {
            return false;
        }

        if (CountAdjacentRooms(roomIndex) > 1)
        {
            return false;
        }

        if (_roomGrid[roomIndex.x, roomIndex.y] != 0)
        {
            return false;
        }

        //add a room
        _roomQueue.Enqueue(roomIndex);
        _roomGrid[roomIndex.x, roomIndex.y] = 1;
        _roomCount++;
        //create it 
        SelectARandomRoomAndInstatiate(roomIndex);

        return true;
    }

    private bool TryGenerateShop(Vector2Int roomIndex)
    {
        if (_roomCount >= maxRoomsCount - 1)
        {
            return false;
        }

        if (_roomGrid[roomIndex.x, roomIndex.y] != 0)
        {
            return false;
        }


        //add a room
        _roomQueue.Enqueue(roomIndex);
        _roomGrid[roomIndex.x, roomIndex.y] = 2;
        _roomCount++;
        //create it 
        var newRoom = Instantiate(lootRoom, GetPositionFromGridIndex(new Vector2Int(roomIndex.x, roomIndex.y)),
            quaternion.identity);
        newRoom.GetComponent<Room>().roomIndex = roomIndex;
        newRoom.name = $"Room-{_roomCount}";
        _roomsObjects.Add(newRoom);
        _isShopSpawned = true;
        OpenDoors(newRoom, roomIndex.x, roomIndex.y);

        return true;
    }


    public void OpenDoors(GameObject room, int x, int y)
    {
        //get the room
        Room newRoomScript = room.GetComponent<Room>();

        //it's Neighbours
        Room leftRoomScript = GetRoomScript(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScript(new Vector2Int(x + 1, y));
        Room upRoomScript = GetRoomScript(new Vector2Int(x, y + 1));
        Room downRoomScript = GetRoomScript(new Vector2Int(x, y - 1));

        //if there is a room to the left -> activate a door beetween them
        if (x > 0 && _roomGrid[x - 1, y] != 0)
        {
            newRoomScript.ActivateDoor(Vector2Int.left);
            leftRoomScript.ActivateDoor(Vector2Int.right);
        }

        //if there is a room to the right -> activate a door beetween them
        if (x < gridSizeX - 1 && _roomGrid[x + 1, y] != 0)
        {
            newRoomScript.ActivateDoor(Vector2Int.right);
            rightRoomScript.ActivateDoor(Vector2Int.left);
        }

        //if there is a room below -> activate a door beetween them
        if (y > 0 && _roomGrid[x, y - 1] != 0)
        {
            newRoomScript.ActivateDoor(Vector2Int.down);
            downRoomScript.ActivateDoor(Vector2Int.up);
        }

        //if there is a room up -> activate a door beetween them
        if (y < gridSizeY - 1 && _roomGrid[x, y + 1] != 0)
        {
            newRoomScript.ActivateDoor(Vector2Int.up);
            upRoomScript.ActivateDoor(Vector2Int.down);
        }
    }

    public void CloseDoors(GameObject room, int x, int y)
    {
        //get the room
        Room newRoomScript = room.GetComponent<Room>();

        //it's Neighbours
        Room leftRoomScript = GetRoomScript(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScript(new Vector2Int(x + 1, y));
        Room upRoomScript = GetRoomScript(new Vector2Int(x, y + 1));
        Room downRoomScript = GetRoomScript(new Vector2Int(x, y - 1));

        newRoomScript.ActivateDoor(Vector2Int.left);
        leftRoomScript.ActivateDoor(Vector2Int.right);

        //if there is a room to the right -> activate a door beetween them
        if (x < gridSizeX - 1 && _roomGrid[x + 1, y] != 0)
        {
            newRoomScript.ActivateDoor(Vector2Int.right);
            rightRoomScript.ActivateDoor(Vector2Int.left);
        }

        //if there is a room below -> activate a door beetween them
        if (y > 0 && _roomGrid[x, y - 1] != 0)
        {
            newRoomScript.ActivateDoor(Vector2Int.down);
            downRoomScript.ActivateDoor(Vector2Int.up);
        }

        //if there is a room up -> activate a door beetween them
        if (y < gridSizeY - 1 && _roomGrid[x, y + 1] != 0)
        {
            newRoomScript.ActivateDoor(Vector2Int.up);
            upRoomScript.ActivateDoor(Vector2Int.down);
        }
    }

    private Room GetRoomScript(Vector2Int index)
    {
        //try to find a room with the right index in all of them iând if right return it
        GameObject accurateRoom = _roomsObjects.Find(r => r.GetComponent<Room>().roomIndex == index);
        if (accurateRoom != null)
        {
            return accurateRoom.GetComponent<Room>();
        }

        return null;
    }

    //regenerate all that needs to be regenerated to try again
    public void RegenerateRooms()
    {
        _roomsObjects.ForEach(Destroy);
        _roomsObjects.Clear();
        _roomGrid = new int[gridSizeX, gridSizeY];
        _roomQueue.Clear();
        _roomCount = 0;
        _generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartWorldGenerationFroomRoom(initialRoomIndex);
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        //count all neighbours
        int count = 0;

        //there is another room at the left
        if (roomIndex.x > 0 && _roomGrid[roomIndex.x - 1, roomIndex.y] != 0)
        {
            count++;
        }

        //there is another room at the right
        if (roomIndex.x < gridSizeX - 1 && _roomGrid[roomIndex.x + 1, roomIndex.y] != 0)
        {
            count++;
        }

        //there is another room below
        if (roomIndex.y > 0 && _roomGrid[roomIndex.x, roomIndex.y - 1] != 0)
        {
            count++;
        }

        //there is another room at the top
        if (roomIndex.y < gridSizeY - 1 && _roomGrid[roomIndex.x, roomIndex.y + 1] != 0)
        {
            count++;
        }

        return count;
    }

    //create position out of grid 
    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int grid_x = gridIndex.x;
        int grid_y = gridIndex.y;

        //return vector with position from grid
        return new Vector3(_roomWith * (grid_x - gridSizeX / 2), _roomHeight * (grid_y - gridSizeY / 2));
    }

    public int GetIndexXFromPosition(Vector3 position)
    {
        int positionX;
        positionX = (int)(position.x + (_roomWith * gridSizeX) / 2) / _roomWith;
        //return vector with position from grid
        return positionX;
    }

    public int GetIndexYFromPosition(Vector3 position)
    {
        int positionY;
        positionY = (int)(position.y + (_roomHeight * gridSizeY) / 2) / _roomHeight;
        //return vector with position from grid
        return positionY;
    }

    //show grid on scene
    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(new Vector3(position.x, position.y), new Vector3(_roomWith, _roomHeight, 1));
            }
        }
    }

    public void DestroyAllRooms()
    {
        foreach (var objects in _roomsObjects)
        {
            DestroyImmediate(objects.gameObject);
        }

        Console.Clear();
    }

    //create a room on a random basis
    private void SelectARandomRoomAndInstatiate(Vector2Int roomIndex)
    {
        var numberOfTheRoom = Random.Range(0, 4);

        //big if cause to setup shop and adjacent rooms
        if (_numberOfIteration == 2)
        {
            if (!_isShopSpawned)
            {
                _roomQueue.Enqueue(roomIndex);
                _roomGrid[roomIndex.x, roomIndex.y] = 1;
                _roomCount++;
                var newRoom = Instantiate(roomFirstVersion, GetPositionFromGridIndex(roomIndex), quaternion.identity);
                newRoom.GetComponent<Room>().roomIndex = roomIndex;
                newRoom.name = $"Room-{_roomCount}";
                _roomsObjects.Add(newRoom);
                OpenDoors(newRoom, roomIndex.x, roomIndex.y);

                if (TryGenerateShop(new Vector2Int(roomIndex.x, roomIndex.y + 1)))
                {
                    _isShopUp = true;
                    Debug.Log("upshop");
                }
                else if (!_isShopUp)
                {
                    if (TryGenerateShop(new Vector2Int(roomIndex.x, roomIndex.y - 1)))
                    {
                        _isShopDown = true;
                        Debug.Log("downshop");
                    }
                }
                else if (!_isShopDown)
                {
                    if (TryGenerateShop(new Vector2Int(roomIndex.x+1, roomIndex.y)))
                    {
                        _isShopRight = true;
                        Debug.Log("rightshop");
                    }
                }
                else if (!_isShopRight)
                {
                    TryGenerateShop(new Vector2Int(roomIndex.x - 1, roomIndex.y));
                    Debug.Log("leftshop");
                }
            }

            _numberOfIteration++;
            return;
        }

        //setup other rooms
        switch (numberOfTheRoom)
        {
            case 0:
                var newRoom = Instantiate(roomFirstVersion, GetPositionFromGridIndex(roomIndex), quaternion.identity);
                newRoom.GetComponent<Room>().roomIndex = roomIndex;
                newRoom.name = $"Room-{_roomCount}";
                _roomsObjects.Add(newRoom);
                OpenDoors(newRoom, roomIndex.x, roomIndex.y);
                break;
            case 1:
                var newRoomSecound = Instantiate(roomSecoundVersion, GetPositionFromGridIndex(roomIndex),
                    quaternion.identity);
                newRoomSecound.GetComponent<Room>().roomIndex = roomIndex;
                newRoomSecound.name = $"Room-{_roomCount}";
                _roomsObjects.Add(newRoomSecound);
                OpenDoors(newRoomSecound, roomIndex.x, roomIndex.y);
                break;
            case 2:
                var newRoomThird = Instantiate(roomThirdVersion, GetPositionFromGridIndex(roomIndex),
                    quaternion.identity);
                newRoomThird.GetComponent<Room>().roomIndex = roomIndex;
                newRoomThird.name = $"Room-{_roomCount}";
                _roomsObjects.Add(newRoomThird);
                OpenDoors(newRoomThird, roomIndex.x, roomIndex.y);
                break;
            case 3:
                var newRoomFourth = Instantiate(roomFourthVersion, GetPositionFromGridIndex(roomIndex),
                    quaternion.identity);
                newRoomFourth.GetComponent<Room>().roomIndex = roomIndex;
                newRoomFourth.name = $"Room-{_roomCount}";
                _roomsObjects.Add(newRoomFourth);
                OpenDoors(newRoomFourth, roomIndex.x, roomIndex.y);
                break;
        }

        _numberOfIteration++;
    }
}