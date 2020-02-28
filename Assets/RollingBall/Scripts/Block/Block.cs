using UnityEngine;

public sealed class Block : BaseBlock, IStageObject
{
    private void Start()
    {
        isMove = false;
    }

    public override void Hit(Vector3 moveDirection)
    {
        base.Hit(moveDirection);
    }

    public void SetPosition(Vector2 initializePosition)
    {
        transform.position = initializePosition;
    }
}