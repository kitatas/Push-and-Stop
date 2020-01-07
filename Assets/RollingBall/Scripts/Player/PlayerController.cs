using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private MoveButton[] moveButtons = null;

    [Inject]
    private void Construct(PlayerMover playerMover, IMoveCountUpdatable moveCountUpdatable, ClearAction clearAction)
    {
        foreach (var moveButton in moveButtons)
        {
            moveButton.OnPushed()
                .Subscribe(_ =>
                {
                    playerMover.MoveAsync(moveButton.MoveDirection()).Forget();
                    moveCountUpdatable.UpdateMoveCount();
                    ActivateButton(false);
                });
        }

        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(_ =>
            {
                playerMover.HitBlock(_);

                var roundPosition = transform.RoundPosition();
                playerMover.UpdatePosition(roundPosition);
                transform
                    .DOMove(roundPosition, ConstantList.correctTime)
                    .OnComplete(playerMover.ResetVelocity);
            });

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