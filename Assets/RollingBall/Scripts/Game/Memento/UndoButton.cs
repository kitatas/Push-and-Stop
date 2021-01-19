using System;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;

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

        public ButtonActivator buttonActivator { get; private set; }

        private void Awake()
        {
            buttonActivator = GetComponent<ButtonActivator>();
        }

        private void Start()
        {
            GetComponent<UnityEngine.UI.Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _subject.OnNext(() => buttonActivator.SetInteractable(false)))
                .AddTo(this);
        }
    }
}