using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace RollingBall.Common.Button
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public sealed class ButtonAnimator : MonoBehaviour
    {
        private void Start()
        {
            var button = GetComponent<UnityEngine.UI.Button>();

            button
                .OnPointerDownAsObservable()
                .Subscribe(_ =>
                {
                    button.image.rectTransform
                        .DOScale(Vector3.one * 0.85f, 0.1f);
                })
                .AddTo(this);

            button
                .OnPointerUpAsObservable()
                .Subscribe(_ =>
                {
                    button.image.rectTransform
                        .DOScale(Vector3.one, 0.1f);
                })
                .AddTo(this);
        }
    }
}