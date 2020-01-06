using UniRx;

public sealed class MoveCountPresenter
{
    public MoveCountPresenter(MoveCountModel moveCountModel, MoveCountView moveCountView)
    {
        moveCountModel.MoveCount()
            .Subscribe(moveCountView.UpdateText);
    }
}