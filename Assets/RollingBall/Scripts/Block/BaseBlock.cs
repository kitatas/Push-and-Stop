﻿using UnityEngine;
using Zenject;

/// <summary>
/// ブロック系の抽象クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBlock : MonoBehaviour, IHittable
{
    private ISeController _seController;

    [Inject]
    private void Construct(ISeController seController)
    {
        _seController = seController;
    }

    private void Awake()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public virtual void Hit(Vector3 moveDirection)
    {
        _seController.PlaySe(SeType.Hit);
    }

    public bool isMove { get; protected set; }
}