using UnityEngine;
using Zenject;

public class LoadButton : BaseButton
{
    [Inject] private readonly ZenjectSceneLoader _sceneLoader = default;
    [SerializeField, SceneName] private string sceneName = null;

    [Inject] private readonly StageDataTable _stageDataTable = default;
    [SerializeField, Range(0, 4)] private int stageNumber = 0;

    protected override void OnPush()
    {
        base.OnPush();

        _sceneLoader.LoadScene(sceneName);

        _stageDataTable.stageIndex = stageNumber;
    }
}