using RollingBall.Utility;
using TMPro;
using UnityEngine;

namespace RollingBall.StageObject
{
    /// <summary>
    /// クリア時のテキストアニメーション
    /// </summary>
    public sealed class ClearText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI clearText = null;
        private const float animationTime = ConstantList.uiAnimationTime;

        public void TweenClearText()
        {
            // var tweener = clearText.GetCharTweener();
            // var characterCount = tweener.CharacterCount;
            // var sequence = DOTween.Sequence();
            //
            // for (var i = 0; i < characterCount; ++i)
            // {
            //     var t = i / (float) characterCount;
            //     var timeOffset = Mathf.Lerp(0f, 1f, t);
            //
            //     var charSequence = DOTween.Sequence();
            //     charSequence
            //         .Append(tweener
            //             .DOLocalMoveY(i, 0.5f, animationTime)
            //             .SetEase(Ease.InOutCubic))
            //         .Join(tweener
            //             .DOFade(i, 0f, animationTime)
            //             .From())
            //         .Join(tweener
            //             .DOScale(i, 0f, animationTime)
            //             .From()
            //             .SetEase(Ease.OutBack, 5f))
            //         .Append(tweener
            //             .DOLocalMoveY(i, 0f, animationTime)
            //             .SetEase(Ease.OutBounce));
            //
            //     sequence.Insert(timeOffset, charSequence);
            // }
        }

        public RectTransform RectTransform() => clearText.rectTransform;
    }
}