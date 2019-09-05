using System;
using UniRx;
using UnityEngine;

public class RotateButton : BaseButton
{
    private readonly Color _activateColor = new Color(0.2f, 0.7f, 0.7f);
    private readonly Color _deactivateColor = new Color(0.1f, 0.4f, 0.5f);

    private readonly Subject<Unit> _subject = new Subject<Unit>();
    public IObservable<Unit> OnPushed() => _subject;

    protected override void OnPush()
    {
        base.OnPush();

        _subject.OnNext(Unit.Default);
    }

    public void ActivateButton(bool value)
    {
        button.enabled = value;
        button.image.color = value ? _activateColor : _deactivateColor;
    }
}