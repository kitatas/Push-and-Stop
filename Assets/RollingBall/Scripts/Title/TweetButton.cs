using System.Linq;
using RollingBall.Common;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;

namespace RollingBall.Title
{
    /// <summary>
    /// ツイートを行うボタン
    /// </summary>
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class TweetButton : MonoBehaviour
    {
        private readonly string _gameId = "nanka_title_v2";
        private readonly string _hashTag = "unityroom";

        private void Start()
        {
            GetComponent<UnityEngine.UI.Button>()
                .OnClickAsObservable()
                .Subscribe(_ => OnPush())
                .AddTo(this);
        }

        private void OnPush()
        {
            var tweetText = $"{GetClearText()}\n";
            tweetText += $"#{_hashTag}\n";
            UnityRoomTweet.Tweet(_gameId, tweetText);
        }

        private static string GetClearText()
        {
            var rankData = RankLoader.GetClearRankData();
            var clearCount = rankData.Count(x => x > 0);
            var clearRankMaxCount = rankData.Count(x => x == 3);
            
            if (clearRankMaxCount == Const.MAX_STAGE_COUNT)
            {
                return $"全てのステージを星３でクリアした！";
            }

            if (clearCount == Const.MAX_STAGE_COUNT)
            {
                return $"全てのステージをクリアした！";
            }

            if (clearCount == 0)
            {
                return $"１つもクリアできてない...";
            }

            return $"ステージ{clearCount}までクリアした！";
        }
    }
}