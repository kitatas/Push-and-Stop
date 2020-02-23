using UniRx.Async;
using UnityEngine;
using Zenject;

public sealed class Transition : MonoBehaviour
{
    private const float alphaCutOffMax = 0.7f;
    private const float alphaCutOffMin = 0f;

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
        var beforeSceneButtons = FindObjectsOfType<BaseButton>();
        beforeSceneButtons.ActivateAllButtons(false);
        SetUpFade(alphaCutOffMax);
        await UniTask.WaitUntil(FadeOut);

        await _zenjectSceneLoader.LoadSceneAsync(sceneName);

        var afterSceneButtons = FindObjectsOfType<BaseButton>();
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
        _transitionProgress += Time.deltaTime;
        var alphaCutOffValue = alphaCutOffMax - alphaCutOffMax * _transitionProgress / _transitionDuration;
        _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

        return IsFadeComplete();
    }

    private bool FadeIn()
    {
        _transitionProgress += Time.deltaTime;
        var alphaCutOffValue = alphaCutOffMax * _transitionProgress / _transitionDuration;
        _transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);

        return IsFadeComplete();
    }

    private bool IsFadeComplete()
    {
        return _transitionProgress >= _transitionDuration;
    }
}