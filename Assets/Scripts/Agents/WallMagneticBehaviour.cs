using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMagneticBehaviour : MonoBehaviour
{
    public enum Direction
    {
        Up, Right, Down, Left
    }

    public Direction WallRepellForceDirection;

    public Vector2 GetWallRepellDirection()
    {
        Vector2 dir = Vector2.zero;
        switch (WallRepellForceDirection)
        {
            case Direction.Up:
                dir = Vector2.up;
                break;
            case Direction.Right:
                dir = Vector2.right;
                break;
            case Direction.Down:
                dir = Vector2.down;
                break;
            case Direction.Left:
                dir = Vector2.left;
                break;
        }

        return dir;
    }
}
