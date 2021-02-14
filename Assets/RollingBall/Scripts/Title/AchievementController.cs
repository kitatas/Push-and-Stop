using System.Linq;
using RollingBall.Common;
using UnityEngine;

namespace RollingBall.Title
{
    public sealed class AchievementController : MonoBehaviour
    {
        [SerializeField] private AchievementButton allClear = default;
        [SerializeField] private AchievementButton allRank = default;

        private void Start()
        {
            var clearData = RankLoader.GetClearRankData();
            var clearCount = clearData.Count(x => x > 0);
            var maxRankClearCount = clearData.Count(x => x == 3);

            allClear.gameObject.SetActive(clearCount == Const.MAX_STAGE_COUNT);
            allRank.gameObject.SetActive(maxRankClearCount == Const.MAX_STAGE_COUNT);
        }

        public void ActivateAllAchievement(bool value)
        {
            allClear.gameObject.SetActive(value);
            allRank.gameObject.SetActive(value);
        }
    }
}