using System;
using Cysharp.Threading.Tasks;
using RollingBall.Button.BaseButton;
using RollingBall.Utility;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace RollingBall.Transition
{
    /// <summary>
    /// シーン遷移を行う
    /// </summary>
    public sealed class SceneLoader
    {
        private const float alphaCutOffMax = 0.7f;
        private const float alphaCutOffMin = 0f;
        private const float fadeSpeedRate = 1.25f;

        private float _transitionProgress;
        private float _transitionDuration;

        private ZenjectSceneLoader _zenjectSceneLoader;
        private TransitionSpriteMask _transitionSpriteMask;

        [Inject]
        private void Construct(ZenjectSceneLoader zenjectSceneLoader, TransitionSpriteMask transitionSpriteMask)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
            _transitionSpriteMask = transitionSpriteMask;
        }

        public void LoadScene(string sceneName)
        {
            _zenjectSceneLoader.LoadScene(sceneName);
        }

        /// <summary>
        /// Loads the Scene by its name in Build Settings.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load</param>
        /// <param name="fadeTime">Time to fade</param>
        public void FadeLoadScene(string sceneName, float fadeTime = 1f)
        {
            _transitionDuration = fadeTime;
            FadeAsync(sceneName).Forget();
        }

        // TODO : キャンセル処理の追加
        private async UniTaskVoid FadeAsync(string sceneName)
        {
            var beforeSceneButtons = Object.FindObjectsOfType<BaseButton>();
            beforeSceneButtons.ActivateAllButtons(false);
            SetUpFade(alphaCutOffMax);
            await UniTask.WaitUntil(() =>
            {
                _transitionProgress += Time.deltaTime * fadeSpeedRate;
                var alphaCutOffValue = alphaCutOffMax - alphaCutOffMax * _transitionProgress / _transitionDuration;
                _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

                return IsFadeComplete();
            });

            await _zenjectSceneLoader.LoadSceneAsync(sceneName);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

            var afterSceneButtons = Object.FindObjectsOfType<BaseButton>();
            afterSceneButtons.ActivateAllButtons(false);
            SetUpFade(alphaCutOffMin);
            await UniTask.WaitUntil(() =>
            {
                _transitionProgress += Time.deltaTime * fadeSpeedRate;
                var alphaCutOffValue = alphaCutOffMax * _transitionProgress / _transitionDuration;
                _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

                return IsFadeComplete();
            });

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