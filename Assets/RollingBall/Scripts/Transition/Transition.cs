using System;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class Transition : MonoBehaviour
{
    private const float _alphaCutOffMax = 0.7f;
    private const float _alphaCutOffMin = 0f;

    private float _transitionProgress;
    private float _transitionDuration;

    private ZenjectSceneLoader _zenjectSceneLoader;
    private TransitionSpriteMask _transitionSpriteMask;

    [Inject]
    private void Construct(ZenjectSceneLoader zenjectSceneLoader, TransitionSpriteMask transitionSpriteMask, SpriteMask spriteMask)
    {
        _zenjectSceneLoader = zenjectSceneLoader;
        _transitionSpriteMask = transitionSpriteMask;

        _transitionSpriteMask.Construct(spriteMask);
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
        var beforeSceneButtons = FindObjectsOfType<Button>();
        beforeSceneButtons.ActivateAllButtons(false);
        SetUpFade(_alphaCutOffMax);
        await UniTask.WaitUntil(FadeOut);

        _zenjectSceneLoader.LoadScene(sceneName);
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));

        var afterSceneButtons = FindObjectsOfType<Button>();
        afterSceneButtons.ActivateAllButtons(false);
        SetUpFade(_alphaCutOffMin);
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