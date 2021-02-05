using DG.Tweening;
using RollingBall.Common;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Title
{
    public sealed class AchievementButton : MonoBehaviour
    {
        [SerializeField] private Image bodyImage = default;
        [SerializeField] private Button button = default;
        [SerializeField] private TextMeshProUGUI achievementText = default;
        private bool _isShow;
        private Sequence _sequence;
        private readonly Vector2 _showSize = new Vector2(350.0f, 100.0f);
        private readonly Vector2 _hideSize = new Vector2(100.0f, 100.0f);

        private void Start()
        {
            _isShow = false;
            button
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (_isShow)
                    {
                        Hide();
                    }
                    else
                    {
                        Show();
                    }
                })
                .AddTo(this);
        }

        private void Show()
        {
            _isShow = true;
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(bodyImage.rectTransform
                    .DOSizeDelta(_showSize, Const.UI_ANIMATION_TIME))
                .Append(achievementText
                    .DOFade(1.0f, Const.UI_ANIMATION_TIME));
        }

        private void Hide()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(achievementText
                    .DOFade(0.0f, Const.UI_ANIMATION_TIME))
                .Append(bodyImage.rectTransform
                    .DOSizeDelta(_hideSize, Const.UI_ANIMATION_TIME))
                .OnComplete(() => _isShow = false);
        }
    }
}