using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 定数管理
/// </summary>
public sealed class ConstantList
{
    public const float uiAnimationTime = 0.5f;
    public const float correctTime = 0.3f;

    public static readonly Dictionary<MoveDirection, Vector3> moveDirection = new Dictionary<MoveDirection, Vector3>
    {
        {MoveDirection.Up,    Vector3.up},
        {MoveDirection.Down,  Vector3.down},
        {MoveDirection.Left,  Vector3.left},
        {MoveDirection.Right, Vector3.right},
    };

    public static readonly Dictionary<PopType, PopInfo> popList = new Dictionary<PopType, PopInfo>
    {
        {PopType.Open,  new PopInfo(true,  1f, Vector3.one, Ease.OutBack)},
        {PopType.Close, new PopInfo(false, 0f, Vector3.one * 0.8f, Ease.OutQuart)},
    };

    public static string GetKeyName(int stageIndex) => $"stage{stageIndex}";
    
    public const int maxStageCount = 30;
}