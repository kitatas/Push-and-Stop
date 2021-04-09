using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using RollingBall.Common;
using RollingBall.Common.Utility;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace RollingBall.Game.StageObject.Block
{
    /// <summary>
    /// 何かにぶつかるまで直進するブロック
    /// </summary>
    public sealed class BallBlock : BaseBlock, IMoveObject
    {
        private bool _isStop;
        private CancellationToken _token;

        private void Start()
        {
            isMove = false;
            _isStop = true;
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

        public override void Hit(Vector2 moveDirection)
        {
            base.Hit(moveDirection);

            MoveAsync(moveDirection, _token).Forget();
        }

        private async UniTaskVoid MoveAsync(Vector2 moveDirection, CancellationToken token)
        {
            isMove = true;
            _isStop = false;
            var moveSpeed = Const.MOVE_SPEED * 0.5f;

            await UniTask.WaitWhile(() =>
            {
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

                return isMove;
            }, cancellationToken: token);
        }

        private void CorrectPosition()
        {
            var roundPosition = transform.RoundPosition();

            transform
                .DOMove(roundPosition, Const.CORRECT_TIME)
                .SetEase(Ease.Linear)
                .OnComplete(() => _isStop = true);
        }

        public void SetPosition(Vector2 setPosition) => transform.position = setPosition;

        public bool isStop => _isStop;
        public Vector3 GetPosition() => transform.position;
    }
}