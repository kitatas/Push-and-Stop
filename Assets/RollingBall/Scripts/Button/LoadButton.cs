using UnityEngine;
using Zenject;

public sealed class LoadButton : BaseButton
{
    [SerializeField, SceneName] private string sceneName = null;
    private const float _fadeTime = 0.7f;

    /// <summary>
    /// -2 : Reload
    /// -1 : Next
    /// over 0 : StageIndex
    /// </summary>
    [SerializeField, Range(-2, 9)] private int stageNumber = 0;

    private Transition _transition;
    private StageDataTable _stageDataTable;

    [Inject]
    private void Construct(Transition transition, StageDataTable stageDataTable)
    {
        _transition = transition;
        _stageDataTable = stageDataTable;
    }

    protected override void OnPush()
    {
        base.OnPush();

        _stageDataTable.stageIndex = GetNextStageIndex();

        _transition.LoadScene(sceneName, _fadeTime);
    }

    private int GetNextStageIndex()
    {
        if (stageNumber >= 0)
        {
            return stageNumber;
        }

        if (stageNumber == -2)
        {
            return _stageDataTable.stageIndex;
        }

        if (++_stageDataTable.stageIndex < _stageDataTable.stageData.Length)
        {
            return _stageDataTable.stageIndex;
        }

        sceneName = "Title";
        return 0;
    }
}