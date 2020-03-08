using System;
using UniRx.Async;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public sealed class Transition
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
    public void LoadScene(string sceneName, float fadeTime = 1f)
    {
        _transitionDuration = fadeTime;
        FadeAsync(sceneName).Forget();
    }

    private async UniTaskVoid FadeAsync(string sceneName)
    {
        var beforeSceneButtons = Object.FindObjectsOfType<BaseButton>();
        beforeSceneButtons.ActivateAllButtons(false);
        SetUpFade(alphaCutOffMax);
        await UniTask.WaitUntil(FadeOut);

        LoadScene(sceneName);
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

        var afterSceneButtons = Object.FindObjectsOfType<BaseButton>();
        afterSceneButtons.ActivateAllButtons(false);
        SetUpFade(alphaCutOffMin);
        await UniTask.WaitUntil(FadeIn);

        afterSceneButtons.ActivateAllButtons(true);
    }

    private void SetUpFade(float alphaCutOffValue)
    {
        _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);
        _transitionProgress = 0f;
    }

    private bool FadeOut()
    {
        _transitionProgress += Time.deltaTime * fadeSpeedRate;
        var alphaCutOffValue = alphaCutOffMax - alphaCutOffMax * _transitionProgress / _transitionDuration;
        _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

        return IsFadeComplete();
    }

    private bool FadeIn()
    {
        _transitionProgress += Time.deltaTime * fadeSpeedRate;
        var alphaCutOffValue = alphaCutOffMax * _transitionProgress / _transitionDuration;
        _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

        return IsFadeComplete();
    }

    private bool IsFadeComplete() => _transitionProgress >= _transitionDuration;
}