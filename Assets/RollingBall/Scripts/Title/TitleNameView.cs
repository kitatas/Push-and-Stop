using DG.Tweening;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace RollingBall.Title
{
    public sealed class TitleNameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleName = default;

        private void Start()
        {
            var highlightColor = new Color(1f, 1f, 0.8f);
            var textAnimation = new DOTweenTMPAnimator(titleName);
            var sequences = new Sequence[textAnimation.textInfo.characterCount];
            for (int i = 0; i < sequences.Length; i++)
            {
                var interval = i * 0.05f;
                sequences[i] = DOTween.Sequence()
                    .AppendInterval(2.0f)
                    .Append(textAnimation
                        .DOColorChar(i, highlightColor, 0.1f)
                        .SetLoops(2, LoopType.Yoyo)
                        .SetDelay(interval))
                    .AppendInterval(3.0f - interval)
                    .SetLoops(-1);
            }

            this.OnDisableAsObservable()
                .Subscribe(_ =>
                {
                    foreach (var sequence in sequences)
                    {
                        sequence?.Kill();
                    }
                })
                .AddTo(this);
        }
    }
}