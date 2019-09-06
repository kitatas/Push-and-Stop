using UnityEngine;
using Zenject;

public class LoadButton : BaseButton
{
    [Inject] private readonly Transition _transition = default;
    [SerializeField, SceneName] private string sceneName = null;

    [Inject] private readonly StageDataTable _stageDataTable = default;
    [SerializeField, Range(-1, 4)] private int stageNumber = 0;

    protected override void OnPush()
    {
        base.OnPush();

        _stageDataTable.stageIndex = LoadIndex();

        _transition.LoadScene(sceneName);
    }

    private int LoadIndex()
    {
        if (stageNumber != -1)
        {
            return stageNumber;
        }

        if (++_stageDataTable.stageIndex < 4)
        {
            return _stageDataTable.stageIndex;
        }

        sceneName = "Title";
        return 0;
    }
}