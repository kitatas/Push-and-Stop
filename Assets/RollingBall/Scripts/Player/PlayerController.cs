using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public sealed class PlayerController : MonoBehaviour, IStageObject
{
    [SerializeField] private MoveButton[] moveButtons = null;
    private PlayerMover _playerMover;
    private GoalInfo _goalInfo;

    [Inject]
    private void Construct(PlayerMover playerMover, GoalInfo goalInfo)
    {
        _playerMover = playerMover;
        _goalInfo = goalInfo;
    }

    private void Start()
    {
        foreach (var moveButton in moveButtons)
        {
            moveButton.OnPushed()
                .Subscribe(moveDirection =>
                {
                    _playerMover.MoveAsync(moveDirection).Forget();
                    moveButtons.ActivateAllButtons(false);
                });
        }

        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(hittable =>
            {
                _playerMover.HitBlock(hittable);

                var roundPosition = transform.RoundPosition();

                if (_goalInfo.EqualPosition(roundPosition))
                {
                    InteractButton(false);
                }

                transform
                    .DOMove(roundPosition, ConstantList.correctTime)
                    .OnComplete(() =>
                    {
                        _playerMover.ResetVelocity();
                        moveButtons.ActivateAllButtons(true);
                    });
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