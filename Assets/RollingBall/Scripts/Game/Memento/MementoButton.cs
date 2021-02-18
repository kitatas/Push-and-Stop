using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Game.Memento
{
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonFader))]
    public sealed class MementoButton : MonoBehaviour
    {
        private readonly Subject<Unit> _subject = new Subject<Unit>();
        public IObservable<Unit> onPush => _subject;

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
                .Subscribe(_ => _subject.OnNext(Unit.Default))
                .AddTo(this);
        }

        public void SetEnabled(bool value) => _buttonActivator.SetEnabled(value);

        public void SetInteractable(bool value) => _buttonActivator.SetInteractable(value);

        public async UniTask HideAsync(CancellationToken token)
        {
            SetInteractable(false);

            await _buttonFader.HideAsync(token);
        }
    }
}