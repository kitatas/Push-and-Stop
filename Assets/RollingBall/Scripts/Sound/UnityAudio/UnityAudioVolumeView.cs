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

    private float bgmDefaultVolumeValue => bgmSlider.maxValue / 2f;
    private float seDefaultVolumeValue => seSlider.maxValue / 2f;

    [Inject]
    private void Construct(UnityAudioBgmController unityAudioBgmController, UnityAudioSeController unityAudioSeController)
    {
        _subject
            .Subscribe(_ => unityAudioSeController.PlaySe(SeType.DecisionButton))
            .AddTo(this);

        Initialize(unityAudioBgmController, unityAudioSeController);
    }

    private void Initialize(IVolumeUpdatable bgm, IVolumeUpdatable se)
    {
        var bgmVolume = ES3.Load(ConstantList.bgmVolumeKey, bgmDefaultVolumeValue);
        bgm.SetVolume(bgmVolume);

        var seVolume = ES3.Load(ConstantList.seVolumeKey, seDefaultVolumeValue);
        se.SetVolume(seVolume);

        SetSliderValue(bgm, se);

        UpdateVolumeSlider(bgm, se);

        OnPushResetButton(bgm, se);
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
                bgm.SetVolume(bgmDefaultVolumeValue);
                se.SetVolume(seDefaultVolumeValue);
                SetSliderValue(bgm, se);

                _subject.OnNext(Unit.Default);
            })
            .AddTo(resetButton);
    }
}