using UnityEngine;
using Zenject;

public class Block : BaseBlock
{
    [Inject] private readonly PlayerController _playerController = default;

    private void Start()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public override void Hit(Vector3 moveDirection)
    {
        base.Hit(moveDirection);

        _playerController.ActivatePlayerButton();
    }
}