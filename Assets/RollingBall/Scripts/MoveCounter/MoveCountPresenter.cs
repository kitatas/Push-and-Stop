using UniRx;
using Zenject;

public class MoveCountPresenter
{
    [Inject]
    public MoveCountPresenter(PlayerMover playerMover, MoveCountModel moveCountModel, MoveCountView moveCountView)
    {
        playerMover
            .OnPushed()
            .Subscribe(_ => moveCountModel.UpdateMoveCount());

        moveCountModel
            .MoveCount()
            .Subscribe(moveCountView.UpdateText);
    }
}