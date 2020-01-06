using UniRx;

public sealed class MoveCountPresenter
{
    public MoveCountPresenter(MoveButton moveButton, MoveCountModel moveCountModel, MoveCountView moveCountView)
    {
        moveButton.OnPushed()
            .Subscribe(_ => moveCountModel.UpdateMoveCount());

        moveCountModel.MoveCount()
            .Subscribe(moveCountView.UpdateText);
    }
}