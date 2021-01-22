using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using RollingBall.Common;
using RollingBall.Common.Utility;
using RollingBall.Game.StageObject;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace RollingBall.Game.Block
{
    /// <summary>
    /// 何かにぶつかるまで直進するブロック
    /// </summary>
    public sealed class BallBlock : BaseBlock, IMoveObject
    {
        private Vector3 _moveDirection;
        private CancellationToken _token;

        private readonly float _moveSpeed = 7.5f;

        private void Start()
        {
            isMove = false;
            _moveDirection = Vector3.zero;
            _token = this.GetCancellationTokenOnDestroy();

            this.OnCollisionEnter2DAsObservable()
                .Select(other => other.gameObject.GetComponent<IHittable>())
                .Where(hittable => hittable != null && hittable.isMove == false)
                .Subscribe(other =>
                {
                    isMove = false;
                    CorrectPosition();
                })
                .AddTo(this);
        }

        public override void Hit(Vector3 moveDirection)
        {
            base.Hit(moveDirection);

            MoveAsync(moveDirection, _token).Forget();
        }

        private async UniTaskVoid MoveAsync(Vector3 moveDirection, CancellationToken token)
        {
            isMove = true;
            _moveDirection = moveDirection;

            await UniTask.WaitWhile(() =>
            {
                transform.position += _moveSpeed * _moveDirection * Time.deltaTime;

                return isMove;
            }, cancellationToken: token);
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