using UnityEngine;

public struct BSPNode
{
    //variables needed
    public Bounds Bounds;
    public BSPGenerator.Direction Direction;

    //constructor
    public BSPNode(Bounds bounds, BSPGenerator.Direction direction)
    {
        Bounds = bounds;
        Direction = direction;
    }

}