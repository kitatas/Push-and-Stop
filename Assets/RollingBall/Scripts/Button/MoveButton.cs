using System;
using UniRx;

public class MoveButton : BaseButton
{
    // private readonly Color _activateColor = new Color(1.0f, 0.9f, 0.8f);
    // private readonly Color _deactivateColor = new Color(0.8f, 0.5f, 0.0f);

    private readonly Subject<Unit> _subject = new Subject<Unit>();
    public IObservable<Unit> OnPushed() => _subject;

    protected override void OnPush()
    {
        base.OnPush();

        ActivateButton(false);

        _subject.OnNext(Unit.Default);
    }

    public void ActivateButton(bool value)
    {
        button.enabled = value;
        button.interactable = value;
        // button.image.color = value ? _activateColor : _deactivateColor;
    }
}