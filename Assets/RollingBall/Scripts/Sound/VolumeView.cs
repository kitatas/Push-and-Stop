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
        IVolumeUpdatable bgm = bgmManager;
        IVolumeUpdatable se = seManager;

        bgmSlider.value = bgm.GetVolume();
        seSlider.value = se.GetVolume();

        bgmSlider
            .OnValueChangedAsObservable()
            .Subscribe(bgm.SetVolume)
            .AddTo(this);

        seSlider
            .OnValueChangedAsObservable()
            .Subscribe(se.SetVolume)
            .AddTo(this);
    }
}