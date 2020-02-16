using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class VolumeView : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider = null;
    [SerializeField] private Slider seSlider = null;

    [Inject]
    private void Construct(BgmManager bgmManager, SeManager seManager)
    {
        Initialize(bgmManager.GetVolume(), seManager.GetVolume());

        bgmSlider
            .OnValueChangedAsObservable()
            .Subscribe(bgmManager.SetVolume)
            .AddTo(this);

        seSlider
            .OnValueChangedAsObservable()
            .Subscribe(seManager.SetVolume)
            .AddTo(this);
    }

    private void Initialize(float bgmVolume, float seVolume)
    {
        bgmSlider.value = bgmVolume;
        seSlider.value = seVolume;
    }
}