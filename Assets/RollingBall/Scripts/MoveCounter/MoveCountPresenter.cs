using UniRx;

public sealed class MoveCountPresenter
{
    public MoveCountPresenter(IMoveCountModel moveCountModel, MoveCountView moveCountView)
    {
        moveCountModel.MoveCount()
            .Subscribe(moveCountView.UpdateText)
            .AddTo(moveCountView);
    }
}