using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using RollingBall.Common;
using RollingBall.Common.Sound.SE;
using RollingBall.Common.Transition;
using RollingBall.Game.StageData;
using RollingBall.Title;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RollingBall.Game.View
{
    /// <summary>
    /// ステージクリア時の演出
    /// </summary>
    public sealed class ClearView : MonoBehaviour
    {
        [SerializeField] private Image rankBackGround = default;
        [SerializeField] private Image[] rankImages = default;
        [SerializeField] private TextMeshProUGUI clearText = default;
        [SerializeField] private LoadButton nextButton = default;
        [SerializeField] private LoadButton reloadButton = default;
        [SerializeField] private LoadButton homeButton = default;

        private static readonly Vector3 _targetRotateVector = new Vector3(0f, 0f, 360f);

        private ISeController _seController;
        private StageRepository _stageRepository;

        [Inject]
        private void Construct(ISeController seController, StageRepository stageRepository)
        {
            _seController = seController;
            _stageRepository = stageRepository;
        }

        public void Show(int moveCount)
        {
            _seController.PlaySe(SeType.Clear);

            var clearRate = (float) moveCount / _stageRepository.GetTargetMoveCount();
            var clearRank = RankLoader.SaveClearData(_stageRepository.GetLevel() - 1, clearRate);

            var token = this.GetCancellationTokenOnDestroy();
            TweenClearAsync(token, clearRank).Forget();
        }

        private async UniTaskVoid TweenClearAsync(CancellationToken token, int clearRank)
        {
            TweenClearRank(clearRank);
            TweenClearText();

            await UniTask.Delay(TimeSpan.FromSeconds(Const.CLEAR_ANIMATION_TIME + 0.5f), cancellationToken: token);

            await clearText.rectTransform
                .DOAnchorPosY(200.0f, Const.UI_ANIMATION_TIME)
                .WithCancellation(token);

            await UniTask.WhenAll(
                nextButton.ShowAsync(token),
                reloadButton.ShowAsync(token),
                homeButton.ShowAsync(token)
            );
        }

        private void TweenClearRank(int clearRank)
        {
            rankBackGround.gameObject.SetActive(true);
            TweenRankImages(clearRank);
        }

        private void TweenRankImages(int count)
        {
            for (int i = 0; i < count; i++)
            {
                TweenRankImage(rankImages[i]);
            }
        }

        private static void TweenRankImage(Image image)
        {
            DOTween.Sequence()
                .Append(image
                    .DOFade(1.0f, Const.CLEAR_ANIMATION_TIME)
                    .SetEase(Ease.InQuad))
                .Join(image.rectTransform
                    .DOScale(Vector3.one, Const.CLEAR_ANIMATION_TIME)
                    .SetEase(Ease.InOutBack))
                .Join(image.rectTransform
                    .DOLocalRotate(_targetRotateVector, Const.CLEAR_ANIMATION_TIME, RotateMode.FastBeyond360)
                    .SetEase(Ease.InQuad));
        }

        private void TweenClearText()
        {
            var textAnimation = new DOTweenTMPAnimator(clearText);
            var offset = Vector3.up * 30.0f;
            for (int i = 0; i < textAnimation.textInfo.characterCount; i++)
            {
                var delayRate = i / (float) textAnimation.textInfo.characterCount;
                var delayTime = Mathf.Lerp(0.0f, 1.0f, delayRate) + (0.0002f * i);

                DOTween.Sequence()
                    .Append(textAnimation
                        .DOOffsetChar(i, textAnimation.GetCharOffset(i) + offset, Const.UI_ANIMATION_TIME)
                        .SetEase(Ease.OutFlash, 2))
                    .Join(textAnimation
                        .DOFadeChar(i, 1, Const.CLEAR_ANIMATION_TIME))
                    .SetDelay(delayTime);
            }
        }
    }
}