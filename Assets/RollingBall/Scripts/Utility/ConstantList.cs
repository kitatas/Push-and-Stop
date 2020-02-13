using System.Collections.Generic;
using UnityEngine;

public sealed class ConstantList
{
    public const float correctTime = 0.3f;

    public const float uiAnimationTime = 0.5f;

    public static readonly Dictionary<Direction, Vector3> moveDirection = new Dictionary<Direction, Vector3>
    {
        {Direction.Up,    Vector3.up},
        {Direction.Down,  Vector3.down},
        {Direction.Left,  Vector3.left},
        {Direction.Right, Vector3.right},
    };
}