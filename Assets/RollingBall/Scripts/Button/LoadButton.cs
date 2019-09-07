using UnityEngine;
using UnityEngine.UI;
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

        foreach (var b in FindObjectsOfType<Button>())
        {
            b.enabled = false;
        }

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

        if (++_stageDataTable.stageIndex < _stageDataTable.stageData.Length)
        {
            return _stageDataTable.stageIndex;
        }

        sceneName = "Title";
        return 0;
    }
}