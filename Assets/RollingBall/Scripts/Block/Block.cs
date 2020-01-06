using UnityEngine;

public sealed class Block : BaseBlock
{
    public override void Hit(Vector3 moveDirection)
    {
        base.Hit(moveDirection);

        ActivatePlayerButton();
    }
}