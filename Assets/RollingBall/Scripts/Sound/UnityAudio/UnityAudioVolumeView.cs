using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class UnityAudioVolumeView : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider = null;
    [SerializeField] private Slider seSlider = null;
    [SerializeField] private Button resetButton = null;

    private readonly Subject<Unit> _subject = new Subject<Unit>();

    [Inject]
    private void Construct(UnityAudioBgmController unityAudioBgmController, UnityAudioSeController unityAudioSeController)
    {
        SetSliderValue(unityAudioBgmController, unityAudioSeController);

        UpdateVolumeSlider(unityAudioBgmController, unityAudioSeController);

        _subject
            .Subscribe(_ => unityAudioSeController.PlaySe(SeType.DecisionButton))
            .AddTo(this);

        OnPushResetButton(unityAudioBgmController, unityAudioSeController);
    }

    private void SetSliderValue(IVolumeUpdatable bgm, IVolumeUpdatable se)
    {
        bgmSlider.value = bgm.GetVolume();
        seSlider.value = se.GetVolume();
    }

    private void UpdateVolumeSlider(IVolumeUpdatable bgm, IVolumeUpdatable se)
    {
        bgmSlider
            .OnValueChangedAsObservable()
            .Subscribe(bgm.SetVolume)
            .AddTo(this);

        seSlider
            .OnValueChangedAsObservable()
            .Subscribe(se.SetVolume)
            .AddTo(this);
    }

    private void OnPushResetButton(IVolumeUpdatable bgm, IVolumeUpdatable se)
    {
        resetButton
            .OnClickAsObservable()
            .Subscribe(_ =>
            {
                bgm.SetVolume(0.5f);
                se.SetVolume(0.5f);
                SetSliderValue(bgm, se);

                _subject.OnNext(Unit.Default);
            })
            .AddTo(resetButton);
    }
}