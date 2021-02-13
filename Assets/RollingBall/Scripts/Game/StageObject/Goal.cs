using System;
using RollingBall.Game.View;
using UniRx;
using UnityEngine;

namespace RollingBall.Game.StageObject
{
    /// <summary>
    /// クリアの判定
    /// </summary>
    public sealed class Goal : MonoBehaviour, IStageObject
    {
        [SerializeField] private ClearView clearView = default;

        private int _currentMoveCount;
        private ReactiveProperty<bool> _isGoal;

        public void Initialize(Action action)
        {
            _currentMoveCount = 0;

            _isGoal = new ReactiveProperty<bool>(false);
            _isGoal
                .Where(x => x)
                .Subscribe(_ =>
                {
                    clearView.Show(_currentMoveCount);
                    action?.Invoke();
                })
                .AddTo(this);
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