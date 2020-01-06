using UniRx;
using UnityEngine;
using Zenject;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private MoveButton[] moveButtons = null;

    [Inject]
    private void Construct(PlayerMover playerMover, MoveCountModel moveCountModel, ClearAction clearAction)
    {
        foreach (var moveButton in moveButtons)
        {
            moveButton.OnPushed()
                .Subscribe(_ =>
                {
                    playerMover.Move(moveButton.MoveDirection()).Forget();
                    moveCountModel.UpdateMoveCount();
                    ActivateButton(false);
                });
        }

        playerMover.OnComplete()
            .Where(clearAction.IsGoalPosition)
            .Subscribe(_ =>
            {
                clearAction.DisplayClearUi();
                ActivateButton(false);
            });
    }

    public void ActivateButton(bool value)
    {
        foreach (var moveButton in moveButtons)
        {
            moveButton.ActivateButton(value);
        }
    }
}