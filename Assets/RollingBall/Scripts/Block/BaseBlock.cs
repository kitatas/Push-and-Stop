using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBlock : MonoBehaviour, IHittable, IStageObject
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

    public bool isMove { get; protected set; }

    public void SetPosition(Vector2 initializePosition)
    {
        transform.position = initializePosition;
    }
}