using DG.Tweening;
using RollingBall.Block;
using RollingBall.Button;
using RollingBall.Button.MoveButton;
using RollingBall.StageObject;
using RollingBall.Utility;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace RollingBall.Player
{
    /// <summary>
    /// プレイヤーの管理
    /// </summary>
    public sealed class PlayerController : MonoBehaviour, IMoveObject
    {
        [SerializeField] private MoveButton[] moveButtons = null;
        private UndoButton _undoButton;
        private PlayerMover _playerMover;
        private IGoal _goal;

        [Inject]
        private void Construct(UndoButton undoButton, PlayerMover playerMover, IGoal goal)
        {
            _undoButton = undoButton;
            _playerMover = playerMover;
            _goal = goal;
        }

        private void Start()
        {
            // 全移動ボタンのSubscribe
            foreach (var moveButton in moveButtons)
            {
                moveButton.OnPushed()
                    .Subscribe(moveDirection =>
                    {
                        _playerMover.Move(moveDirection);
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

                    if (_goal.IsEqualPosition(roundPosition))
                    {
                        InteractMoveButton(false);
                        _undoButton.InteractButton(false);
                    }
                    else
                    {
                        _undoButton.InteractButton(true);
                    }

                    CorrectPosition(roundPosition);
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

        private void CorrectPosition(Vector2 roundPosition)
        {
            transform
                .DOMove(roundPosition, ConstantList.correctTime)
                .OnComplete(() =>
                {
                    _playerMover.ResetVelocity();
                    _undoButton.ActivateButton(true);
                    moveButtons.ActivateAllButtons(true);
                });
        }

        public void SetPosition(Vector2 setPosition)
        {
            transform.position = setPosition;
        }

        public Vector3 GetPosition() => transform.position;
    }
}