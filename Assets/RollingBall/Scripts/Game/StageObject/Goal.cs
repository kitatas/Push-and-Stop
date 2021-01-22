using UnityEngine;

namespace RollingBall.Game.StageObject
{
    /// <summary>
    /// クリアの判定
    /// </summary>
    public sealed class Goal : MonoBehaviour, IGoal
    {
        public void SetPosition(Vector2 setPosition) => transform.position = setPosition;

        private Vector2 GetPosition() => transform.position;

        public bool IsEqualPosition(Vector2 roundPosition) => GetPosition() == roundPosition;
    }
}