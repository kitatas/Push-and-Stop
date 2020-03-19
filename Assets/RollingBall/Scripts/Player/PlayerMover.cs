using UnityEngine;

public sealed class PlayerMover
{
    private readonly Rigidbody2D _rigidbody;

    private Vector3 _moveDirection;
    private const float _moveSpeed = 10f;
    private readonly Vector2 _zero = Vector2.zero;

    private PlayerMover(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        _moveDirection = Vector3.zero;
    }

    public void Move(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;

        _rigidbody.velocity = _moveSpeed * _moveDirection;
    }

    public void HitBlock(IHittable hittable)
    {
        hittable.Hit(_moveDirection);
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = _zero;
    }
}