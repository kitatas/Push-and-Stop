using UnityEngine;
using Zenject;

public abstract class BaseBlock : MonoBehaviour, IHittable
{
    [Inject] private readonly SeManager _seManager = default;

    public virtual void Hit(Vector3 moveDirection)
    {
        _seManager.PlaySe(SeType.Hit);
    }
}