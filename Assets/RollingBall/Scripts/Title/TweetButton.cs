using System.Linq;
using RollingBall.Common;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace RollingBall.Title
{
    /// <summary>
    /// ツイートを行うボタン
    /// </summary>
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class TweetButton : MonoBehaviour
    {
        private const string GAME_ID = "nanka_title_v2";
        private const string GAME_NAME = "Push_and_Stop";
        private const string HASH_TAG = "unityroom";

        private void Start()
        {
            GetComponent<UnityEngine.UI.Button>()
                .OnClickAsObservable()
                .Subscribe(_ => OnPush())
                .AddTo(this);
        }

        private static void OnPush()
        {
            var tweetText = $"{GetClearText()}\n";

            tweetText += $"#{GAME_NAME}\n";
            tweetText += $"https://play.google.com/store/apps/details?id=com.KitaLab.PushStop";
            var url = $"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(tweetText)}";
            Application.OpenURL(url);
        }

        private static string GetClearText()
        {
            var rankData = RankLoader.GetClearRankData();
            var clearCount = rankData.Count(x => x > 0);

            switch (clearCount)
            {
                case Const.MAX_STAGE_COUNT:
                    var maxRankClearCount = rankData.Count(x => x == 3);
                    var clearStatus = maxRankClearCount == Const.MAX_STAGE_COUNT ? "★３で" : "";
                    return $"全てのステージを{clearStatus}クリアした！";
                case 0:
                    return $"１つもクリアできてない...";
                default:
                    return $"ステージ{clearCount}までクリアした！";
            }
        }
    }
}