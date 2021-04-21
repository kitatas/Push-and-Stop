using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using RollingBall.Common;
using RollingBall.Common.Sound.SE;
using RollingBall.Game.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace RollingBall.Game.StageObject
{
    /// <summary>
    /// クリアの判定
    /// </summary>
    public sealed class Goal : MonoBehaviour, IStageObject
    {
        [SerializeField] private ClearView clearView = default;
        private int _targetMoveCount;

        private int _currentMoveCount;
        private ReactiveProperty<bool> _isGoal;

        private ISeController _seController;

        [Inject]
        private void Construct(ISeController seController)
        {
            _seController = seController;
        }

        public void Initialize(Action action)
        {
            _currentMoveCount = 0;
            var token = this.GetCancellationTokenOnDestroy();

            _isGoal = new ReactiveProperty<bool>(false);
            _isGoal
                .Where(x => x)
                .Subscribe(_ =>
                {
                    action?.Invoke();
                    TweenClearAsync(token).Forget();
                })
                .AddTo(this);
        }

        public void SetTargetMoveCount(int targetMoveCount)
        {
            _targetMoveCount = targetMoveCount;
        }

        private async UniTaskVoid TweenClearAsync(CancellationToken token)
        {
            await TweenGoalAsync(token);

            var clearRate = (float) _currentMoveCount / _targetMoveCount;
            clearView.Show(clearRate);
        }

        private async UniTask TweenGoalAsync(CancellationToken token)
        {
            _seController.PlaySe(SeType.Goal);

            await DOTween.Sequence()
                .Append(transform
                    .DOScale(new Vector3(75.0f, 75.0f, 1.0f), Const.UI_ANIMATION_TIME)
                    .SetEase(Ease.Linear))
                .Join(transform
                    .DOLocalRotate(new Vector3(0.0f, 0.0f, -180f), Const.UI_ANIMATION_TIME)
                    .SetOptions(false)
                    .SetEase(Ease.Linear))
                .WithCancellation(token);
        }

        public void SetPosition(Vector2 setPosition) => transform.position = setPosition;

        private Vector2 GetPosition() => transform.position;

        public void SetPlayerPosition(Vector2 playerPosition, int moveCount)
        {
            _currentMoveCount = moveCount;
            _isGoal.Value = GetPosition() == playerPosition;
        }
    }
}