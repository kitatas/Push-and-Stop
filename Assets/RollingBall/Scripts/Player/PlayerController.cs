using System;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public sealed class PlayerController : MonoBehaviour, IStageObject
{
    [SerializeField] private MoveButton[] moveButtons = null;

    [Inject]
    private void Construct(PlayerMover playerMover, IMoveCountUpdatable moveCountUpdatable, GoalInfo goalInfo, ClearAction clearAction)
    {
        foreach (var moveButton in moveButtons)
        {
            moveButton.OnPushed()
                .Subscribe(moveDirection =>
                {
                    playerMover.MoveAsync(moveDirection).Forget();
                    moveCountUpdatable.UpdateMoveCount();
                    moveButtons.ActivateAllButtons(false);
                });
        }

        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(hittable =>
            {
                playerMover.HitBlock(hittable);

                var roundPosition = transform.RoundPosition();

                transform
                    .DOMove(roundPosition, ConstantList.correctTime)
                    .OnComplete(playerMover.ResetVelocity);

                var isClear = goalInfo.EqualPosition(roundPosition);
                if (isClear)
                {
                    InteractButton(false);
                    clearAction.DisplayClearUi();
                    return;
                }

                Observable
                    .Timer(TimeSpan.FromSeconds(ConstantList.correctTime))
                    .Subscribe(_ => moveButtons.ActivateAllButtons(true));
            })
            .AddTo(gameObject);
    }

    private void InteractButton(bool value)
    {
        foreach (var moveButton in moveButtons)
        {
            moveButton.InteractButton(value);
        }
    }

    public void SetPosition(Vector2 initializePosition)
    {
        transform.position = initializePosition;
    }
}