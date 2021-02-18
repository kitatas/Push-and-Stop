using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Game.Player
{
    /// <summary>
    /// プレイヤーの移動を行うボタン
    /// </summary>
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonFader))]
    public sealed class MoveButton : MonoBehaviour
    {
        [SerializeField] private MoveDirection moveDirection = default;

        private readonly Subject<MoveDirection> _subject = new Subject<MoveDirection>();
        public IObservable<MoveDirection> onPush => _subject;

        private ButtonActivator _buttonActivator;
        private ButtonFader _buttonFader;

        private void Awake()
        {
            _buttonActivator = GetComponent<ButtonActivator>();
            _buttonFader = GetComponent<ButtonFader>();
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _subject.OnNext(moveDirection))
                .AddTo(this);
        }

        public void SetEnabled(bool value) => _buttonActivator.SetEnabled(value);

        public void SetInteractable(bool value) => _buttonActivator.SetInteractable(value);

        public void Hide(CancellationToken token) => _buttonFader.HideAsync(token).Forget();

    }
}