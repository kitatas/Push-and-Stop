using System;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;
using Zenject;

namespace RollingBall.Common.Transition
{
    /// <summary>
    /// シーン遷移を行うボタン
    /// </summary>
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonSpeaker))]
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public sealed class LoadButton : MonoBehaviour
    {
        [SerializeField] private LoadType loadType = default;
        [HideInInspector] public int stageNumber;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            GetComponent<UnityEngine.UI.Button>()
                .OnClickAsObservable()
                .Subscribe(_ => LoadScene())
                .AddTo(this);
        }

        public LoadType GetLoadType() => loadType;

        private void LoadScene()
        {
            switch (loadType)
            {
                case LoadType.Direct:
                    _sceneLoader.FadeLoadScene(SceneName.Main, stageNumber, Const.FADE_TIME);
                    break;
                case LoadType.Reload:
                    _sceneLoader.LoadScene(SceneName.Main, stageNumber);
                    break;
                case LoadType.Next:
                    LoadNext();
                    break;
                case LoadType.Title:
                    LoadTitle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(loadType), loadType, null);
            }
        }

        private void LoadNext()
        {
            // if (_stageDataTable.IsNextStage())
            {
                _sceneLoader.FadeLoadScene(SceneName.Main, stageNumber + 1, Const.FADE_TIME);
                return;
            }

            // LoadTitle();
        }

        private void LoadTitle()
        {
            // _stageDataTable.ResetStageIndex();
            _sceneLoader.FadeLoadScene(SceneName.Title, 0, Const.FADE_TIME);
        }
    }
}