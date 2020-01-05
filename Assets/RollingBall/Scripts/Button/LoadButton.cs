using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadButton : BaseButton
{
    private Transition _transition;
    [SerializeField, SceneName] private string sceneName = null;
    [SerializeField] private float fadeTime = 0.7f;

    private StageDataTable _stageDataTable;

    /// <summary>
    /// -2 : Reload
    /// -1 : Next
    /// over 0 : StageIndex
    /// </summary>
    [SerializeField, Range(-2, 9)] private int stageNumber = 0;

    [Inject]
    private void Construct(Transition transition, StageDataTable stageDataTable)
    {
        _transition = transition;
        _stageDataTable = stageDataTable;
    }

    protected override void OnPush()
    {
        base.OnPush();

        foreach (var b in FindObjectsOfType<Button>())
        {
            b.enabled = false;
        }

        _stageDataTable.stageIndex = LoadIndex();

        _transition.LoadScene(sceneName, fadeTime);
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