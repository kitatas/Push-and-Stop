using RollingBall.Button.BaseButton;
using RollingBall.Utility;

namespace RollingBall.Button
{
    /// <summary>
    /// ツイートを行うボタン
    /// </summary>
    public sealed class TweetButton : BaseButton.BaseButton
    {
        private readonly string _gameId = "nanka_title_v2";
        private readonly string _hashTag = "unityroom";

        protected override void OnPush(ButtonType buttonType)
        {
            base.OnPush(ButtonType.Decision);

            var clearInfo = GetClearInfo();
            var tweetText = $"{GetMainTweetText(clearInfo)}\n";
            tweetText += $"#{_hashTag}\n";
            // UnityRoomTweet.Tweet(_gameId, tweetText);
        }

        /// <summary>
        /// セーブデータからクリア状況を取得
        /// </summary>
        /// <returns></returns>
        private static (int, int) GetClearInfo()
        {
            var clearStageIndex = 0;
            var completeCount = 0;

            for (int i = 0; i < ConstantList.maxStageCount; i++)
            {
                var key = ConstantList.GetKeyName(i);
                var clearInfo = ES3.Load(key, 0);
                if (clearInfo == 0)
                {
                    break;
                }

                if (clearInfo == 3)
                {
                    completeCount++;
                }

                clearStageIndex = i + 1;
            }

            return (clearStageIndex, completeCount);
        }

        private static string GetMainTweetText((int, int) clearInfo)
        {
            var (clearStageIndex, completeCount) = clearInfo;

            if (completeCount == ConstantList.maxStageCount)
            {
                return $"全てのステージを星３でクリアした！";
            }

            if (clearStageIndex == ConstantList.maxStageCount)
            {
                return "全てのステージをクリアした！";
            }

            if (clearStageIndex == 0)
            {
                return "１つもクリアできてない...";
            }

            return $"ステージ{clearStageIndex}までクリアした！";
        }
    }
}