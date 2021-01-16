using UnityEngine;

namespace RollingBall.Transition
{
    /// <summary>
    /// シーン遷移のマスク管理
    /// </summary>
    public sealed class TransitionSpriteMask
    {
        private readonly SpriteMask _spriteMask;

        public TransitionSpriteMask(SpriteMask spriteMask)
        {
            _spriteMask = spriteMask;
        }

        public void SetAlphaCutOff(float alphaCutOffValue)
        {
            _spriteMask.alphaCutoff = Mathf.Clamp01(alphaCutOffValue);
        }
    }
}