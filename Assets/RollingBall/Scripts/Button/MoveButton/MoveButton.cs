using System;
using UniRx;
using UnityEngine;
using Zenject;

public sealed class MoveButton : BaseButton
{
    private readonly Subject<Vector3> _subject = new Subject<Vector3>();
    public IObservable<Vector3> OnPushed() => _subject;

    [SerializeField] private MoveDirection moveDirection = default;

    private IMoveCountUpdatable _moveCountUpdatable;

    [Inject]
    private void Construct(IMoveCountUpdatable moveCountUpdatable)
    {
        _moveCountUpdatable = moveCountUpdatable;
    }

    protected override void OnPush()
    {
        base.OnPush();

        _moveCountUpdatable.UpdateMoveCount(UpdateType.Increase);

        _subject.OnNext(ConstantList.moveDirection[moveDirection]);
    }

    public void InteractButton(bool value)
    {
        button.interactable = value;
    }
}