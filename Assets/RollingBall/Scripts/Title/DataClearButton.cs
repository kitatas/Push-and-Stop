using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Title
{
    /// <summary>
    /// セーブデータを削除するボタン
    /// </summary>
    [RequireComponent(typeof(Button))]
    public sealed class DataClearButton : MonoBehaviour
    {
        [SerializeField] private RankLoader rankLoader = default;
        [SerializeField] private AchievementController achievementController = default;

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    rankLoader.ResetClearRank();
                    achievementController.ActivateAllAchievement(false);
                })
                .AddTo(this);
        }
    }
}