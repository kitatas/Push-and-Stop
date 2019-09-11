using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBlock : MonoBehaviour, IHittable
{
    [Inject] private readonly SeManager _seManager = default;
    [Inject] private readonly PlayerController _playerController = default;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public virtual void Hit(Vector3 moveDirection)
    {
        _seManager.PlaySe(SeType.Hit);
    }

    protected void ActivatePlayerButton()
    {
        _playerController.ActivatePlayerButton();
    }
}