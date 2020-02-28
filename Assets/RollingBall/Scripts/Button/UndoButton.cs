using UnityEngine;
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
        if (_caretaker.IsMementoStackEmpty())
        {
            return;
        }

        base.OnPush();

        _moveCountUpdatable.UpdateMoveCount(UpdateType.Decrease);

        _caretaker.PopMementoStack();
    }

    public void InteractButton(bool value)
    {
        button.interactable = false;
    }
}