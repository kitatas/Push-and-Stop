using RollingBall.Common.Sound.SE;
using UnityEngine;
using Zenject;

namespace RollingBall.Game.StageObject.Block
{
    /// <summary>
    /// ブロック系の抽象クラス
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseBlock : MonoBehaviour, IHittable
    {
        private ISeController _seController;

        [Inject]
        private void Construct(ISeController seController)
        {
            _seController = seController;
        }

        private void Awake()
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public virtual void Hit(Vector2 moveDirection)
        {
            _seController.PlaySe(SeType.Hit);
        }

        public bool isMove { get; protected set; }
    }
}