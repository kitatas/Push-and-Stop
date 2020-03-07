using Zenject;

public sealed class UndoButton : BaseButton
{
    private IMoveCountUpdatable _moveCountUpdatable;
    private Caretaker _caretaker;

    [Inject]
    private void Construct(IMoveCountUpdatable moveCountUpdatable, Caretaker caretaker)
    {
        _moveCountUpdatable = moveCountUpdatable;
        _caretaker = caretaker;
    }

    protected override void OnPush()
    {
        base.OnPush();

        _moveCountUpdatable.UpdateMoveCount(UpdateType.Decrease);

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