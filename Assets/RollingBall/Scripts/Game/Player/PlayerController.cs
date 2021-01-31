using DG.Tweening;
using RollingBall.Common;
using RollingBall.Common.Utility;
using RollingBall.Game.Block;
using RollingBall.Game.Clear;
using RollingBall.Game.Memento;
using RollingBall.Game.MoveCount;
using RollingBall.Game.StageObject;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace RollingBall.Game.Player
{
    /// <summary>
    /// プレイヤーの管理
    /// </summary>
    public sealed class PlayerController : MonoBehaviour, IMoveObject
    {
        [SerializeField] private MoveButton[] moveButtons = default;
        [SerializeField] private UndoButton undoButton = default;
        [SerializeField] private ClearView clearView = default;

        private PlayerMover _playerMover;
        private IMoveCountUseCase _moveCountUseCase;
        private Goal _goal;

        [Inject]
        private void Construct(PlayerMover playerMover, IMoveCountUseCase moveCountUseCase, Goal goal)
        {
            _playerMover = playerMover;
            _moveCountUseCase = moveCountUseCase;
            _goal = goal;
        }

        private void Start()
        {
            // 全移動ボタン
            foreach (var moveButton in moveButtons)
            {
                moveButton.onPush
                    .Subscribe(moveDirection =>
                    {
                        _moveCountUseCase.CountUp();
                        _playerMover.Move(moveDirection);
                        SetEnableButton(false);
                    })
                    .AddTo(moveButton);
            }

            // 一手戻るボタン
            undoButton.onPush
                .Where(_ => _moveCountUseCase.CountDown())
                .Subscribe(action => action?.Invoke())
                .AddTo(undoButton);

            this.OnCollisionEnter2DAsObservable()
                .Select(other => other.gameObject.GetComponent<IHittable>())
                .Where(hittable => hittable != null)
                .Subscribe(hittable =>
                {
                    _playerMover.HitBlock(hittable);

                    var roundPosition = transform.RoundPosition();

                    if (_goal.IsEqualPosition(roundPosition))
                    {
                        SetInteractableButton(false);
                        clearView.Show(_moveCountUseCase.currentCount);
                    }
                    else
                    {
                        undoButton.SetInteractable(true);
                    }

                    CorrectPosition(roundPosition);
                })
                .AddTo(this);
        }

        private void CorrectPosition(Vector2 roundPosition)
        {
            transform
                .DOMove(roundPosition, Const.CORRECT_TIME)
                .OnComplete(() =>
                {
                    _playerMover.ResetVelocity();
                    SetEnableButton(true);
                });
        }

        private void SetEnableButton(bool value)
        {
            undoButton.SetEnabled(value);
            foreach (var moveButton in moveButtons)
            {
                moveButton.SetEnabled(value);
            }
        }

        private void SetInteractableButton(bool value)
        {
            undoButton.SetInteractable(value);
            foreach (var moveButton in moveButtons)
            {
                moveButton.SetInteractable(value);
            }
        }

        public void SetPosition(Vector2 setPosition) => transform.position = setPosition;

        public Vector3 GetPosition() => transform.position;
    }
}