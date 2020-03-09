using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class UnityAudioVolumeView : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider = null;
    [SerializeField] private Slider seSlider = null;
    [SerializeField] private Button resetButton = null;

    [Inject]
    private void Construct(UnityAudioBgmController unityAudioBgmController, UnityAudioSeController unityAudioSeController)
    {
        IVolumeUpdatable bgm = unityAudioBgmController;
        IVolumeUpdatable se = unityAudioSeController;

        void SetSliderValue()
        {
            bgmSlider.value = bgm.GetVolume();
            seSlider.value = se.GetVolume();
        }

        SetSliderValue();

        bgmSlider
            .OnValueChangedAsObservable()
            .Subscribe(bgm.SetVolume)
            .AddTo(this);

        seSlider
            .OnValueChangedAsObservable()
            .Subscribe(se.SetVolume)
            .AddTo(this);

        resetButton
            .OnClickAsObservable()
            .Subscribe(_ =>
            {
                bgm.SetVolume(0.5f);
                se.SetVolume(0.5f);

                SetSliderValue();

                unityAudioSeController.PlaySe(SeType.Button);
            })
            .AddTo(this);
    }
}