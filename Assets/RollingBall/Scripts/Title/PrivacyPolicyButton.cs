using RollingBall.Common.Button;
using UniRx;
using UnityEngine;

namespace RollingBall.Title
{
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class PrivacyPolicyButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<UnityEngine.UI.Button>()
                .OnClickAsObservable()
                .Subscribe(_ => OnPush())
                .AddTo(this);
        }

        private static void OnPush()
        {
            var url = $"https://kitatas.github.io/Push-and-Stop/";
            Application.OpenURL(url);
        }
    }
}