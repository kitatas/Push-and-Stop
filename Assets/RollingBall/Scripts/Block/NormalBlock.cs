﻿using UnityEngine;

/// <summary>
/// 移動しないブロック
/// </summary>
public sealed class NormalBlock : BaseBlock, IStageObject
{
    private void Start()
    {
        isMove = false;
    }

    public void SetPosition(Vector2 setPosition)
    {
        transform.position = setPosition;
    }
}