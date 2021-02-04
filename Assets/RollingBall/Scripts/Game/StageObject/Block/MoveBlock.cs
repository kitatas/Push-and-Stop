using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using RollingBall.Common;
using RollingBall.Common.Utility;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace RollingBall.Game.StageObject.Block
{
    /// <summary>
    /// １マス分移動するブロック
    /// </summary>
    public sealed class MoveBlock : BaseBlock, IMoveObject
    {
        private TweenerCore<Vector3, Vector3, VectorOptions> _tweenCore;

        private void Start()
        {
            isMove = false;

            this.OnCollisionEnter2DAsObservable()
                .Select(other => other.gameObject.GetComponent<IHittable>())
                .Where(hittable => hittable != null && hittable.isMove == false)
                .Subscribe(_ =>
                {
                    isMove = false;
                    _tweenCore?.Kill();
                    CorrectPosition();
                })
                .AddTo(this);
        }

        public override void Hit(Vector3 moveDirection)
        {
            base.Hit(moveDirection);

            Move(moveDirection);
        }

        private void Move(Vector3 moveDirection)
        {
            isMove = true;
            var nextPosition = transform.position + moveDirection;

            _tweenCore = transform
                .DOMove(nextPosition, Const.CORRECT_TIME)
                .SetEase(Ease.Linear)
                .OnComplete(() => isMove = false);
        }

        private void CorrectPosition()
        {
            var roundPosition = transform.RoundPosition();

            transform
                .DOMove(roundPosition, Const.CORRECT_TIME)
                .SetEase(Ease.Linear);
        }

        public void SetPosition(Vector2 setPosition) => transform.position = setPosition;

        public Vector3 GetPosition() => transform.position;
    }
}