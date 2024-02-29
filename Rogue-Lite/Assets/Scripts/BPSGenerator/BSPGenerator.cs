using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BSPGenerator : MonoBehaviour
{
    //enum
    public enum Direction
    {
        Horizontal,
        Vertical,
        CompleteRandom
    }


    //SerializeField/public variables
    [SerializeField] private Vector2 size;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileBase tileBase;
    [SerializeField] [Range(0f, 1f)] private float ratio = 0.5f;
    [SerializeField] private Direction direction;
    [SerializeField] private int minArea;


    public void Generate()
    {
        //define bounds for big square
        Bounds b = new Bounds();
        Vector3 min, max;
        min.x = -1 * size.x / 2f;
        max.x = +1 * size.x / 2f;
        min.y = -1 * size.x / 2f;
        max.y = +1 * size.x / 2f;
        min.z = 0;
        max.z = 1;
        b.SetMinMax(min, max);

        //define else that allow us to cut and stuff
        Queue<BSPNode> queue = new Queue<BSPNode>();
        List<BoundsInt> rooms = new List<BoundsInt>();
        Direction cutDirection = direction;

        //loop for the cuts
        queue.Enqueue(new BSPNode(b, cutDirection));
        while (queue.Count > 0 && rooms.Count < 5000)
        {
            BSPNode node = queue.Dequeue();


            Cut(node.Bounds, out var boundsA, out var boundsB, ratio, node.Direction);

            if (boundsA.size.x * boundsA.size.y > minArea)
            {
                queue.Enqueue(new BSPNode(boundsA, SwapDirection(node.Direction)));
            }
            else
            {
                BoundsInt toAdd = new BoundsInt();
                Vector3Int minBoundA = new Vector3Int((int)boundsA.min.x, (int)boundsA.min.y, (int)boundsA.min.z);
                Vector3Int maxBoundA = new Vector3Int((int)boundsA.max.x, (int)boundsA.max.y, (int)boundsA.max.z);
                toAdd.SetMinMax(minBoundA, maxBoundA);
                rooms.Add(toAdd);
            }

            if (boundsB.size.x * boundsB.size.y > minArea)
            {
                queue.Enqueue(new BSPNode(boundsB, SwapDirection(node.Direction)));
            }
            else
            {
                BoundsInt toAdd = new BoundsInt();
                Vector3Int minBoundB = new Vector3Int((int)boundsB.min.x, (int)boundsB.min.y, (int)boundsB.min.z);
                Vector3Int maxBoundB = new Vector3Int((int)boundsB.max.x, (int)boundsB.max.y, (int)boundsB.max.z);
                toAdd.SetMinMax(minBoundB, maxBoundB);
                rooms.Add(toAdd);
            }
        }

        Clear();
        foreach (var room in rooms)
        {
            PaintMap(room, tileMap, tileBase, Color.green);
        }
    }

    private static Direction SwapDirection(Direction cutDirection)
    {
        //switch direction each rounds
        if (cutDirection == Direction.Horizontal)
        {
            cutDirection = Direction.Vertical;
        }
        else if (cutDirection == Direction.Vertical)
        {
            cutDirection = Direction.Horizontal;
        }

        return cutDirection;
    }

    //paint map
    private void PaintMap(BoundsInt allTiles, Tilemap map, TileBase tile, Color color)
    {
        color = ColorRandomSwap();
        foreach (Vector3Int pos in allTiles.allPositionsWithin)
        {
            map.SetTile(pos, tile);

            //setupp complete random color for each tile
            switch (direction)
            {
                case Direction.CompleteRandom:
                    color = ColorRandomSwap();
                    break;
            }

            map.SetColor(pos, color);
        }
    }

    //generate a random color
    private Color ColorRandomSwap()
    {
        Color color;

        Color randomColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );
        color = randomColor;
        return color;
    }

    //cut tiles
    private void Cut(Bounds inBounds, out Bounds boundsB, out Bounds boundsA, float ratio, Direction direction)
    {
        //setup
        boundsA = inBounds;
        boundsB = inBounds;

        //effect of the direction button
        switch (direction)
        {
            case Direction.Horizontal:
                float middleY = inBounds.min.y + inBounds.size.y * ratio;
                boundsA.SetMinMax(inBounds.min, new Vector3(inBounds.max.x, middleY, inBounds.max.z));
                boundsB.SetMinMax(new Vector3(inBounds.min.x, middleY, inBounds.min.z), inBounds.max);
                break;
            case Direction.Vertical:
                float middleX = inBounds.min.x + inBounds.size.x * ratio;
                boundsA.SetMinMax(inBounds.min, new Vector3(middleX, inBounds.max.y, inBounds.max.z));
                boundsB.SetMinMax(new Vector3(middleX, inBounds.min.y, inBounds.min.z), inBounds.max);
                break;
            case Direction.CompleteRandom:
                //random number that decide the cut
                float randomCut = Random.Range(0, 100);
                //horizontal
                if (randomCut >= 50)
                {
                    ratio = randomCut / 100;
                    float middleHorizontal = inBounds.min.y + inBounds.size.y * ratio;
                    boundsA.SetMinMax(inBounds.min, new Vector3(inBounds.max.x, middleHorizontal, inBounds.max.z));
                    boundsB.SetMinMax(new Vector3(inBounds.min.x, middleHorizontal, inBounds.min.z), inBounds.max);
                }
                else //vertical
                {
                    ratio = randomCut / 100;
                    float middleVertical = inBounds.min.x + inBounds.size.x * ratio;
                    boundsA.SetMinMax(inBounds.min, new Vector3(middleVertical, inBounds.max.y, inBounds.max.z));
                    boundsB.SetMinMax(new Vector3(middleVertical, inBounds.min.y, inBounds.min.z), inBounds.max);
                }

                ;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Direction), direction, null);
        }
    }

    //clear tiles
    public void Clear()
    {
        tileMap.ClearAllTiles();
    }
}