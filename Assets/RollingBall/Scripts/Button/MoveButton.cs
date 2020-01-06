using System;
using UniRx;
using UnityEngine;

public class MoveButton : BaseButton
{
    private readonly Subject<Unit> _subject = new Subject<Unit>();
    public IObservable<Unit> OnPushed() => _subject;

    [SerializeField] private Direction direction = default;

    public Vector3 MoveDirection() => ConstantList.moveDirection[direction];

    protected override void OnPush()
    {
        base.OnPush();

        _subject.OnNext(Unit.Default);
    }

    public void ActivateButton(bool value)
    {
        button.enabled = value;
        button.interactable = value;
    }
}