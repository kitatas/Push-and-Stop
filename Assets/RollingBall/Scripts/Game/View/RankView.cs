using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MPUIKIT;
using RollingBall.Common;
using RollingBall.Common.Utility;
using UnityEngine;

namespace RollingBall.Game.View
{
    public enum Side
    {
        Center,
        Left2,
        Left3,
        Right2,
        Right3,
    }

    public sealed class RankView : MonoBehaviour
    {
        [SerializeField] private MPImage star = default;
        private static readonly int _gradientRotation = Shader.PropertyToID("_GradientRotation");
        private static readonly float _starSpace = 325.0f;

        public async UniTask TweenStarAsync(Side side, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(star.rectTransform
                    .DOAnchorPosX(TweenPosition(side), 0.0f))
                .Append(star
                    .DOFade(1.0f, Const.CLEAR_ANIMATION_TIME)
                    .SetEase(Ease.OutCubic))
                .Join(star.rectTransform
                    .DOScale(Vector3.one * 3.0f, Const.CLEAR_ANIMATION_TIME)
                    .SetEase(Ease.OutBack))
                .WithCancellation(token);

            TweenStarGradient();
        }

        private void TweenStarGradient()
        {
            DOTween.Sequence()
                .Append(DOTween.To(
                        () => star.material.GetFloat(_gradientRotation),
                        value => star.material.SetFloat(_gradientRotation, value),
                        -405.0f,
                        2.0f)
                    .SetEase(Ease.InOutCirc))
                .SetLoops(-1, LoopType.Restart)
                .DisableKill(this);
        }

        private static float TweenPosition(Side side)
        {
            switch (side)
            {
                case Side.Center:
                    return 0.0f;
                case Side.Left2:
                    return _starSpace * -0.5f;
                case Side.Left3:
                    return _starSpace * -1.0f;
                case Side.Right2:
                    return _starSpace * 0.5f;
                case Side.Right3:
                    return _starSpace;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }
    }
}