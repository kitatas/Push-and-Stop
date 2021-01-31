using System;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Game.Memento
{
    /// <summary>
    /// 一手前に戻るボタン
    /// </summary>
    [RequireComponent(typeof(ButtonActivator))]
    public sealed class UndoButton : MonoBehaviour
    {
        private readonly Subject<Action> _subject = new Subject<Action>();
        public IObservable<Action> onPush => _subject;

        private ButtonActivator _buttonActivator;

        private void Awake()
        {
            _buttonActivator = GetComponent<ButtonActivator>();
        }

        private void Start()
        {
            SetInteractable(false);

            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _subject.OnNext(() => SetInteractable(false)))
                .AddTo(this);
        }

        public void SetEnabled(bool value) => _buttonActivator.SetEnabled(value);

        public void SetInteractable(bool value) => _buttonActivator.SetInteractable(value);
    }
}