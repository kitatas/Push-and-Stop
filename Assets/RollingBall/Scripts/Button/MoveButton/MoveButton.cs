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
    private Caretaker _caretaker;

    [Inject]
    private void Construct(IMoveCountUpdatable moveCountUpdatable, Caretaker caretaker)
    {
        _moveCountUpdatable = moveCountUpdatable;
        _caretaker = caretaker;
    }

    protected override void OnPush(ButtonType buttonType)
    {
        base.OnPush(ButtonType.Decision);

        _moveCountUpdatable.UpdateMoveCount(UpdateType.Increase);

        _caretaker.PushMementoStack();

        _subject.OnNext(ConstantList.moveDirection[moveDirection]);
    }

    public void InteractButton(bool value)
    {
        button.interactable = value;
    }
}