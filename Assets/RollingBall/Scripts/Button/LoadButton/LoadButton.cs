using System;
using UnityEngine;
using Zenject;

public sealed class LoadButton : BaseButton
{
    [SerializeField, SceneName] private string sceneName = null;
    private const float _fadeTime = 0.7f;

    [SerializeField] private LoadType loadType = default;
    [HideInInspector] public int stageNumber;

    private SceneLoader _sceneLoader;
    private StageDataTable _stageDataTable;

    [Inject]
    private void Construct(SceneLoader sceneLoader, StageDataTable stageDataTable)
    {
        _sceneLoader = sceneLoader;
        _stageDataTable = stageDataTable;
    }

    protected override void OnPush()
    {
        base.OnPush();

        LoadScene();
    }

    public LoadType GetLoadType() => loadType;

    private void LoadScene()
    {
        switch (GetLoadType())
        {
            case LoadType.Direct:
                _stageDataTable.SetStageIndex(stageNumber);
                _sceneLoader.FadeLoadScene(sceneName, _fadeTime);
                break;
            case LoadType.Reload:
                _sceneLoader.LoadScene(sceneName);
                break;
            case LoadType.Next:
                LoadNext();
                break;
            case LoadType.Title:
                LoadTitle();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void LoadNext()
    {
        if (_stageDataTable.IsNextStage())
        {
            _sceneLoader.FadeLoadScene(sceneName, _fadeTime);
            return;
        }

        LoadTitle();
    }

    private void LoadTitle()
    {
        _stageDataTable.ResetStageIndex();
        _sceneLoader.FadeLoadScene("Title", _fadeTime);
    }
}