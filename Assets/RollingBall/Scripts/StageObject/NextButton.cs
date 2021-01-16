using DG.Tweening;
using RollingBall.Utility;
using UnityEngine;

namespace RollingBall.StageObject
{
    /// <summary>
    /// 次ステージをロードするボタンのアニメーション
    /// </summary>
    public sealed class NextButton : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button nextButton = null;
        private const float animationTime = ConstantList.uiAnimationTime;

        public void Initialize()
        {
            nextButton.interactable = false;
        }

        public void DisplayNextButton(RectTransform clearText)
        {
            nextButton.enabled = false;
            nextButton.interactable = true;

            DOTween.Sequence()
                .AppendInterval(animationTime * 2f + 0.5f)
                .Append(clearText
                    .DOAnchorPosY(70f, animationTime))
                .Append(nextButton.image
                    .DOFade(1f, animationTime))
                .OnComplete(() => nextButton.enabled = true);
        }
    }
}