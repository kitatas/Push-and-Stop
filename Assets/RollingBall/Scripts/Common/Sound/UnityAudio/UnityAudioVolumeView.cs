using RollingBall.Common.Sound.SE;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RollingBall.Common.Sound.UnityAudio
{
    /// <summary>
    /// 音量の調整
    /// </summary>
    public sealed class UnityAudioVolumeView : MonoBehaviour
    {
        [SerializeField] private Slider bgmSlider = null;
        [SerializeField] private Slider seSlider = null;
        [SerializeField] private UnityEngine.UI.Button resetButton = null;

        [Inject]
        private void Construct(UnityAudioBgmController unityAudioBgmController, UnityAudioSeController unityAudioSeController)
        {
            seSlider
                .OnPointerUpAsObservable()
                .Subscribe(_ => unityAudioSeController.PlaySe(SeType.Decision))
                .AddTo(seSlider);

            SetSliderValue(unityAudioBgmController, unityAudioSeController);

            UpdateVolumeSlider(unityAudioBgmController, unityAudioSeController);

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
                    bgm.SetVolume(bgmSlider.maxValue / 2.0f);
                    se.SetVolume(seSlider.maxValue / 2.0f);
                    SetSliderValue(bgm, se);
                })
                .AddTo(resetButton);
        }
    }
}