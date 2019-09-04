using UniRx;
using Zenject;

public class MoveCountPresenter
{
    [Inject]
    public MoveCountPresenter(MoveButton moveButton, MoveCountModel moveCountModel, MoveCountView moveCountView)
    {
        moveButton
            .OnPushed()
            .Subscribe(_ => moveCountModel.UpdateMoveCount());

        moveCountModel
            .MoveCount()
            .Subscribe(moveCountView.UpdateText);
    }
}