using System;
using UniRx.Async;
using UnityEngine;
using Zenject;

public class Transition : MonoBehaviour
{
	[Inject] private readonly ZenjectSceneLoader _zenjectSceneLoader = default;
	[Inject] private readonly TransitionSpriteMask _transitionSpriteMask = default;
	private static bool _isFading = false;

	private const float _alphaCutOffMax = 0.7f;
	private const float _alphaCutOffMin = 0f;

	private float _transitionProgress;
	private float _transitionDuration;


	/// <summary>
	/// Loads the Scene by its name in Build Settings.
	/// </summary>
	/// <param name="sceneName">Name of the scene to load</param>
	/// <param name="fadeTime">Time to fade</param>
	public void LoadScene(string sceneName, float fadeTime = 1f)
	{
		if (_isFading)
		{
			return;
		}

		_isFading = true;

		_transitionDuration = fadeTime;
		FadeCoroutine(sceneName).Forget();
	}

	private async UniTaskVoid FadeCoroutine(string sceneName)
	{
		SetUpFade(_alphaCutOffMax);

		while (true)
		{
			FadeOut();

			if (IsFadeComplete())
			{
				break;
			}

			await UniTask.Yield();
		}

		SetUpFade(_alphaCutOffMin);
		_zenjectSceneLoader.LoadScene(sceneName);
		await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

		while (true)
		{
			FadeIn();

			if (IsFadeComplete())
			{
				break;
			}

			await UniTask.Yield();
		}

		_isFading = false;
	}

	private void SetUpFade(float alphaCutOffValue)
	{
		_transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);
		_transitionProgress = 0f;
	}

	private void FadeOut()
	{
		_transitionProgress += Time.deltaTime;
		var alphaCutOffValue = _alphaCutOffMax - _alphaCutOffMax * _transitionProgress / _transitionDuration;
		_transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);
	}

	private void FadeIn()
	{
		_transitionProgress += Time.deltaTime;
		var alphaCutOffValue = _alphaCutOffMax * _transitionProgress / _transitionDuration;
		_transitionSpriteMask.SetAlphaCutOff(alphaCutOffValue);
	}

	private bool IsFadeComplete()
	{
		return _transitionProgress >= _transitionDuration;
	}
}