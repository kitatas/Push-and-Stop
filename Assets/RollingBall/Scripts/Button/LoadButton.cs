using UnityEngine;
using Zenject;

public class LoadButton : BaseButton
{
    [Inject] private readonly Transition _transition = default;
    [SerializeField, SceneName] private string sceneName = null;

    [Inject] private readonly StageDataTable _stageDataTable = default;

    /// <summary>
    /// -2 : Reload
    /// -1 : Next
    /// over 0 : StageIndex
    /// </summary>
    [SerializeField, Range(-2, 4)] private int stageNumber = 0;

    protected override void OnPush()
    {
        base.OnPush();

        _stageDataTable.stageIndex = LoadIndex();

        _transition.LoadScene(sceneName, 0.7f);
    }

    private int LoadIndex()
    {
        if (stageNumber >= 0)
        {
            return stageNumber;
        }

        if (stageNumber == -2)
        {
            return _stageDataTable.stageIndex;
        }

        if (++_stageDataTable.stageIndex < 4)
        {
            return _stageDataTable.stageIndex;
        }

        sceneName = "Title";
        return 0;
    }
}