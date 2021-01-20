using System;
using RollingBall.Common.Sound.SE;
using UniRx;
using UnityEngine;
using Zenject;

namespace RollingBall.Common.Button
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public sealed class ButtonSpeaker : MonoBehaviour
    {
        [SerializeField] private ButtonType buttonType = default;

        private ISeController _seController;

        [Inject]
        private void Construct(ISeController seController)
        {
            _seController = seController;
        }

        private void Start()
        {
            GetComponent<UnityEngine.UI.Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _seController.PlaySe(GetSeType(buttonType)))
                .AddTo(this);
        }

        private static SeType GetSeType(ButtonType type)
        {
            switch (type)
            {
                case ButtonType.Decision:
                    return SeType.Decision;
                case ButtonType.Cancel:
                    return SeType.Cancel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}