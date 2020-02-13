using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBlock : MonoBehaviour, IHittable
{
    private SeManager _seManager;

    [Inject]
    private void Construct(SeManager seManager)
    {
        _seManager = seManager;
    }

    private void Awake()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public virtual void Hit(Vector3 moveDirection)
    {
        _seManager.PlaySe(SeType.Hit);
    }
}