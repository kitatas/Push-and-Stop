using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RollingBall.Common.Button;
using RollingBall.Common.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace RollingBall.Common.Transition
{
    /// <summary>
    /// シーン遷移を行う
    /// </summary>
    public sealed class SceneLoader
    {
        private readonly float _alphaCutOffMax = 0.7f;
        private readonly float _alphaCutOffMin = 0f;
        private readonly float _fadeSpeedRate = 1.25f;

        private float _transitionProgress;
        private float _transitionDuration;

        private ZenjectSceneLoader _zenjectSceneLoader;
        private TransitionSpriteMask _transitionSpriteMask;
        private CancellationToken _token;

        public SceneLoader()
        {

        }

        ~SceneLoader()
        {

        }

        [Inject]
        private void Construct(ZenjectSceneLoader zenjectSceneLoader, TransitionSpriteMask transitionSpriteMask)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
            _transitionSpriteMask = transitionSpriteMask;
        }

        public void LoadScene(SceneName sceneName, int level)
        {
            _zenjectSceneLoader.LoadScene(sceneName.ToString(), LoadSceneMode.Single, container =>
            {
                container.BindInstance(level);
            });
        }

        public void FadeLoadScene(SceneName sceneName, int level, float fadeTime)
        {
            _transitionDuration = fadeTime;
            FadeLoadAsync(sceneName.ToString(), level, _token).Forget();
        }

        private async UniTaskVoid FadeLoadAsync(string sceneName, int level, CancellationToken token)
        {
            var beforeSceneButtons = Object.FindObjectsOfType<ButtonActivator>();
            beforeSceneButtons.ActivateAllButtons(false);
            SetUpFade(_alphaCutOffMax);
            await UniTask.WaitUntil(() =>
            {
                _transitionProgress += Time.deltaTime * _fadeSpeedRate;
                var alphaCutOffValue = _alphaCutOffMax - _alphaCutOffMax * _transitionProgress / _transitionDuration;
                _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

                return IsFadeComplete();
            }, cancellationToken: token);

            await _zenjectSceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Single, container =>
            {
                container.BindInstance(level);
            });

            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);

            var afterSceneButtons = Object.FindObjectsOfType<ButtonActivator>();
            afterSceneButtons.ActivateAllButtons(false);
            SetUpFade(_alphaCutOffMin);
            await UniTask.WaitUntil(() =>
            {
                _transitionProgress += Time.deltaTime * _fadeSpeedRate;
                var alphaCutOffValue = _alphaCutOffMax * _transitionProgress / _transitionDuration;
                _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

                return IsFadeComplete();
            }, cancellationToken: token);

            afterSceneButtons.ActivateAllButtons(true);
        }

        private void SetUpFade(float alphaCutOffValue)
        {
            _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);
            _transitionProgress = 0f;
        }

        private bool IsFadeComplete() => _transitionProgress >= _transitionDuration;
    }
}