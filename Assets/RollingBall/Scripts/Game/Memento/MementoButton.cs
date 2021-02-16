using System;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Game.Memento
{
    [RequireComponent(typeof(ButtonActivator))]
    public sealed class MementoButton : MonoBehaviour
    {
        private readonly Subject<Unit> _subject = new Subject<Unit>();
        public IObservable<Unit> onPush => _subject;

        private ButtonActivator _buttonActivator;

        private void Awake()
        {
            _buttonActivator = GetComponent<ButtonActivator>();
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
    }
}