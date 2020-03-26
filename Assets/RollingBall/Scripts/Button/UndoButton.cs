using Zenject;

public sealed class UndoButton : BaseButton
{
    private IMoveCountUpdatable _moveCountUpdatable;
    private ICaretakerPopable _caretaker;

    [Inject]
    private void Construct(IMoveCountUpdatable moveCountUpdatable, ICaretakerPopable caretaker)
    {
        _moveCountUpdatable = moveCountUpdatable;
        _caretaker = caretaker;

        InteractButton(false);
    }

    protected override void OnPush(ButtonType buttonType)
    {
        base.OnPush(ButtonType.Cancel);

        _moveCountUpdatable.UpdateMoveCount(UpdateType.Decrease);

        _caretaker.PopMementoStack();

        if (_caretaker.IsMementoStackEmpty())
        {
            InteractButton(false);
        }
    }

    public void InteractButton(bool value)
    {
        button.interactable = value;
    }
}