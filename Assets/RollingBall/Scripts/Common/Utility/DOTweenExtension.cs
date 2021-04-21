using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace RollingBall.Common.Utility
{
    public static class DOTweenExtension
    {
        public static void DisableKill(this Sequence sequence, MonoBehaviour monoBehaviour)
        {
            monoBehaviour.OnDisableAsObservable()
                .Subscribe(_ => sequence?.Kill())
                .AddTo(monoBehaviour);
        }
    }
}