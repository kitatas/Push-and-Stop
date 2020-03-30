public sealed class TweetButton : BaseButton
{
    private readonly string _gameId = "nanka_title_v2";
    private readonly string _hashTag = "";
    private const int _maxStageCount = 30;

    protected override void OnPush(ButtonType buttonType)
    {
        base.OnPush(ButtonType.Decision);

        var clearInfo = GetClearInfo();
        var tweetText = $"{GetMainTweetText(clearInfo)}";
        tweetText += $"{_hashTag}\n";
        UnityRoomTweet.Tweet(_gameId, tweetText);
    }

    private static (int, int) GetClearInfo()
    {
        var clearStageIndex = 0;
        var completeCount = 0;

        for (int i = 0; i < _maxStageCount; i++)
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

        if (completeCount == _maxStageCount)
        {
            return $"全てのステージを星３でクリアした！\n";
        }

        if (clearStageIndex == _maxStageCount)
        {
            return "全てのステージをクリアした！\n";
        }

        if (clearStageIndex == 0)
        {
            return "１つもクリアできてない...\n";
        }

        return $"ステージ{clearStageIndex}までクリアした！";
    }
}