using Cysharp.Threading.Tasks;
using DG.Tweening;
using RollingBall.Common;
using RollingBall.Common.Utility;
using RollingBall.Game.Memento;
using RollingBall.Game.MoveCount;
using RollingBall.Game.StageObject;
using RollingBall.Game.StageObject.Block;
using RollingBall.Game.View;
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
        [SerializeField] private MementoButton undoButton = default;
        [SerializeField] private MementoButton resetButton = default;
        [SerializeField] private HomeButton homeButton = default;

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
            _moveCountUseCase
                .WhereMoveCount(0)
                .Subscribe(_ => SetInteractableMementoButton(false))
                .AddTo(this);

            _moveCountUseCase
                .WhereMoveCount(1)
                .Subscribe(_ => SetInteractableMementoButton(true))
                .AddTo(this);

            var token = this.GetCancellationTokenOnDestroy();
            _goal.Initialize(() =>
            {
                undoButton.HideAsync(token).Forget();
                resetButton.HideAsync(token).Forget();
                homeButton.HideAsync(token).Forget();
                foreach (var moveButton in moveButtons)
                {
                    moveButton.HideAsync(token).Forget();
                }
            });

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
                .Subscribe(_ => _moveCountUseCase.CountDown())
                .AddTo(undoButton);

            // 移動リセットボタン
            resetButton.onPush
                .Subscribe(_ => _moveCountUseCase.ResetCount())
                .AddTo(resetButton);

            this.OnCollisionEnter2DAsObservable()
                .Select(other => other.gameObject.GetComponent<IHittable>())
                .Where(hittable => hittable != null)
                .Subscribe(hittable =>
                {
                    _playerMover.HitBlock(hittable);
                    var roundPosition = transform.RoundPosition();
                    _goal.SetPlayerPosition(roundPosition, _moveCountUseCase.currentCount);
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

        private void SetInteractableMementoButton(bool value)
        {
            undoButton.SetInteractable(value);
            resetButton.SetInteractable(value);
        }

        private void SetEnableButton(bool value)
        {
            undoButton.SetEnabled(value);
            resetButton.SetEnabled(value);
            homeButton.SetEnabled(value);
            foreach (var moveButton in moveButtons)
            {
                moveButton.SetEnabled(value);
            }
        }

        public void SetPosition(Vector2 setPosition) => transform.position = setPosition;

        public Vector3 GetPosition() => transform.position;
    }
}