using System;
using UniRx.Async;
using UnityEngine;
using Zenject;

public class Transition : MonoBehaviour
{
    private const float _alphaCutOffMax = 0.7f;
    private const float _alphaCutOffMin = 0f;

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
        SetUpFade(_alphaCutOffMax);

        await UniTask.WaitUntil(FadeOut);

        SetUpFade(_alphaCutOffMin);
        _zenjectSceneLoader.LoadScene(sceneName);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        await UniTask.WaitUntil(FadeIn);
    }

    private void SetUpFade(float alphaCutOffValue)
    {
        _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);
        _transitionProgress = 0f;
    }

    private bool FadeOut()
    {
        _transitionProgress += Time.deltaTime;
        var alphaCutOffValue = _alphaCutOffMax - _alphaCutOffMax * _transitionProgress / _transitionDuration;
        _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

        return IsFadeComplete();
    }

    private bool FadeIn()
    {
        _transitionProgress += Time.deltaTime;
        var alphaCutOffValue = _alphaCutOffMax * _transitionProgress / _transitionDuration;
        _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

        return IsFadeComplete();
    }

    private bool IsFadeComplete()
    {
        return _transitionProgress >= _transitionDuration;
    }
}