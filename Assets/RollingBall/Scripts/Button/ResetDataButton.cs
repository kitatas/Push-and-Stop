using RollingBall.Button.BaseButton;
using RollingBall.StageData;
using RollingBall.Utility;
using UnityEngine;

namespace RollingBall.Button
{
    /// <summary>
    /// セーブデータを削除するボタン
    /// </summary>
    public sealed class ResetDataButton : BaseButton.BaseButton
    {
        [SerializeField] private RankLoader[] rankLoaders = null;

        protected override void OnPush(ButtonType buttonType)
        {
            for (int i = 0; i < ConstantList.maxStageCount; i++)
            {
                var key = ConstantList.GetKeyName(i);
                ES3.Save<int>(key, 0);
            }

            foreach (var rankLoader in rankLoaders)
            {
                rankLoader.LoadRank();
            }
        }
    }
}