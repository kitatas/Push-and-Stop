using System;
using UniRx;
using UnityEngine;

public sealed class MoveButton : BaseButton
{
    private readonly Subject<Vector3> _subject = new Subject<Vector3>();
    public IObservable<Vector3> OnPushed() => _subject;

    [SerializeField] private MoveDirection moveDirection = default;

    protected override void OnPush()
    {
        base.OnPush();

        _subject.OnNext(ConstantList.moveDirection[moveDirection]);
    }

    public void InteractButton(bool value)
    {
        button.interactable = value;
    }
}