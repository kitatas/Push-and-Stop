using UnityEngine;

namespace RollingBall.Memento
{
    /// <summary>
    /// ステージ内の１つのオブジェクトの位置を保持
    /// </summary>
    public sealed class Memento
    {
        private readonly Vector3 _position;

        public Memento(Vector3 position)
        {
            _position = position;
        }

        public Vector3 GetPosition() => _position;
    }
}