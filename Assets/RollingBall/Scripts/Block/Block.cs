using UnityEngine;

public sealed class Block : BaseBlock
{
    private void Start()
    {
        isMove = false;
    }

    public override void Hit(Vector3 moveDirection)
    {
        base.Hit(moveDirection);
    }
}