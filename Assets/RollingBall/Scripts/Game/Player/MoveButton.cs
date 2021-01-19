using System;
using RollingBall.Common.Button;
using UniRx;
using UnityEngine;

namespace RollingBall.Game.Player
{
    /// <summary>
    /// プレイヤーの移動を行うボタン
    /// </summary>
    [RequireComponent(typeof(ButtonActivator))]
    public sealed class MoveButton : MonoBehaviour
    {
        [SerializeField] private MoveDirection moveDirection = default;

        private readonly Subject<MoveDirection> _subject = new Subject<MoveDirection>();
        public IObservable<MoveDirection> onPush => _subject;

        public ButtonActivator buttonActivator { get; private set; }

        private void Awake()
        {
            buttonActivator = GetComponent<ButtonActivator>();
        }

        private void Start()
        {
            GetComponent<UnityEngine.UI.Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _subject.OnNext(moveDirection))
                .AddTo(this);
        }
    }
}