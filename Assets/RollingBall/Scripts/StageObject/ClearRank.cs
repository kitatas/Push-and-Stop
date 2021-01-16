using DG.Tweening;
using RollingBall.MoveCounter;
using RollingBall.StageData;
using RollingBall.Utility;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RollingBall.StageObject
{
    /// <summary>
    /// 移動回数からクリアランクを保存
    /// </summary>
    public sealed class ClearRank : MonoBehaviour
    {
        [SerializeField] private Image rankBackGround = null;
        [SerializeField] private Image[] rankImages = null;
        private const float animationTime = ConstantList.uiAnimationTime * 2f;

        private IMoveCount _moveCount;
        private int _stageIndex;
        private int _minMoveCount;

        [Inject]
        private void Construct(IMoveCount moveCount, StageDataTable stageDataTable, TargetMoveCountView targetMoveCountView)
        {
            _moveCount = moveCount;
            _stageIndex = stageDataTable.StageIndex();
            _minMoveCount = stageDataTable.StageDataInfo().targetCount;

            targetMoveCountView.Display(_minMoveCount);
        }

        public void DisplayClearRank()
        {
            rankBackGround.gameObject.SetActive(true);

            var clearRate = (float) _moveCount.moveCount / _minMoveCount;
            var clearRank = GetClearRank(clearRate);
            TweenRankImages(clearRank);

            var key = ConstantList.GetKeyName(_stageIndex);
            var loadRank = ES3.Load(key, 0);
            if (clearRank > loadRank)
            {
                ES3.Save<int>(key, clearRank);
            }
        }

        private static int GetClearRank(float clearRate)
        {
            if (clearRate <= 1.0f)
            {
                return 3;
            }

            if (clearRate <= 1.5f)
            {
                return 2;
            }

            return 1;
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
                    .DOFade(1f, animationTime)
                    .SetEase(Ease.InQuad))
                .Join(image.rectTransform
                    .DOScale(Vector3.one, animationTime)
                    .SetEase(Ease.InOutBack))
                .Join(image.rectTransform
                    .DOLocalRotate(new Vector3(0f, 0f, 360f), animationTime, RotateMode.FastBeyond360)
                    .SetEase(Ease.InQuad));
        }
    }
}