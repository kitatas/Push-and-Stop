using System;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;

namespace RollingBall.Title
{
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class ExternalSiteButton : MonoBehaviour
    {
        private enum ExternalSiteType
        {
            None,
            Credit,
            License,
            Policy,
        }

        [SerializeField] private ExternalSiteType type = default;

        private void Start()
        {
            var external = type switch
            {
                ExternalSiteType.Credit => "credit",
                ExternalSiteType.License => "license",
                ExternalSiteType.Policy => "policy",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
            };

            var url = $"https://kitatas.github.io/games/push_and_stop/{external}";

            GetComponent<UnityEngine.UI.Button>()
                .OnClickAsObservable()
                .Subscribe(_ => Application.OpenURL(url))
                .AddTo(this);
        }
    }
}