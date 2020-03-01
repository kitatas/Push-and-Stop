using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public sealed class PlayerController : MonoBehaviour, IMoveObject
{
    [SerializeField] private MoveButton[] moveButtons = null;
    private UndoButton _undoButton;
    private Caretaker _caretaker;
    private PlayerMover _playerMover;
    private IGoal _goal;

    [Inject]
    private void Construct(UndoButton undoButton, Caretaker caretaker, PlayerMover playerMover, IGoal goal)
    {
        _undoButton = undoButton;
        _caretaker = caretaker;
        _playerMover = playerMover;
        _goal = goal;
    }

    private void Start()
    {
        foreach (var moveButton in moveButtons)
        {
            moveButton.OnPushed()
                .Subscribe(moveDirection =>
                {
                    _caretaker.PushMementoStack();
                    _playerMover.MoveAsync(moveDirection).Forget();
                    _undoButton.ActivateButton(false);
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

                if (_goal.EqualPosition(roundPosition))
                {
                    InteractMoveButton(false);
                    _undoButton.InteractButton(false);
                }
                else
                {
                    _undoButton.InteractButton(true);
                }

                transform
                    .DOMove(roundPosition, ConstantList.correctTime)
                    .OnComplete(() =>
                    {
                        _playerMover.ResetVelocity();
                        _undoButton.ActivateButton(true);
                        moveButtons.ActivateAllButtons(true);
                    });
            })
            .AddTo(gameObject);
    }

    private void InteractMoveButton(bool value)
    {
        foreach (var moveButton in moveButtons)
        {
            moveButton.InteractButton(value);
        }
    }

    public void SetPosition(Vector2 setPosition)
    {
        transform.position = setPosition;
    }

    public Vector3 GetPosition() => transform.position;
}