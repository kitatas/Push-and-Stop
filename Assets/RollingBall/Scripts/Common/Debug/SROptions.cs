using System.ComponentModel;
using RollingBall.Common;
using RollingBall.Title;
using UnityEngine.SceneManagement;

public partial class SROptions
{
    private const string CLEAR_INFO = "クリア状態";

    [Category(CLEAR_INFO)]
    [Sort(0)]
    [DisplayName("ステージ数")]
    [NumberRange(1, 100)]
    [Increment(1)]
    public int stageLevel { get; set; } = 1;

    [Category(CLEAR_INFO)]
    [Sort(1)]
    [DisplayName("クリア実行")]
    public void ExecClear()
    {
        var clearData = RankLoader.GetClearRankData();
        for (int i = 0; i < Const.MAX_STAGE_COUNT; i++)
        {
            if (i < stageLevel)
            {
                clearData[i] = 3;
            }
            else if (i == stageLevel)
            {
                clearData[i] = 0;
            }
            else
            {
                clearData[i] = -1;
            }
        }

        RankLoader.Save(clearData);
        SceneManager.LoadScene("Title");
    }
}