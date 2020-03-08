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

        LoadScene();
    }

    private void LoadScene()
    {
        // Direct Load
        if (stageNumber >= 0)
        {
            _stageDataTable.SetStageIndex(stageNumber);
            _transition.LoadScene(sceneName, _fadeTime);
            return;
        }

        // Reload
        if (stageNumber == -2)
        {
            _transition.LoadScene(sceneName);
            return;
        }

        // Next Load
        if (_stageDataTable.IsNextStage())
        {
            _transition.LoadScene(sceneName, _fadeTime);
            return;
        }

        _stageDataTable.ResetStageIndex();
        _transition.LoadScene("Title", _fadeTime);
    }
}