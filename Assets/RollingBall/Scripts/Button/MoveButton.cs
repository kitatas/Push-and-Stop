using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : BaseButton
{
    private Image _image;

    private readonly Subject<Unit> _subject = new Subject<Unit>();
    public IObservable<Unit> OnPushed() => _subject;

    protected override void Awake()
    {
        base.Awake();

        _image = button.GetComponent<Image>();
    }

    protected override void OnPush()
    {
        base.OnPush();

        _subject.OnNext(Unit.Default);
    }

    public void ActivateButton(bool value)
    {
        button.enabled = value;
        _image.color = value ? Color.white : Color.gray;
    }
}