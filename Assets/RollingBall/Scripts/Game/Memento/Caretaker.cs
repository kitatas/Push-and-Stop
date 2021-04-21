using System.Collections.Generic;
using System.Linq;
using RollingBall.Game.StageObject;

namespace RollingBall.Game.Memento
{
    /// <summary>
    /// ステージ内で移動するオブジェクトの位置を保持
    /// </summary>
    public sealed class Caretaker
    {
        private readonly Stack<Memento[]> _mementoStack;
        private readonly MoveObjectEntity _moveObjectEntity;

        public Caretaker(MoveObjectEntity moveObjectEntity)
        {
            _mementoStack = new Stack<Memento[]>();
            _moveObjectEntity = moveObjectEntity;
        }

        /// <summary>
        /// 移動するオブジェクトの位置をStackに追加
        /// </summary>
        public void PushMementoStack()
        {
            var mementoArray = new Memento[_moveObjectEntity.Count()];
            for (int i = 0; i < _moveObjectEntity.Count(); i++)
            {
                mementoArray[i] = new Memento(_moveObjectEntity.Get(i).GetPosition());
            }

            _mementoStack.Push(mementoArray);
        }

        /// <summary>
        /// Stackから移動するオブジェクトの位置を更新
        /// </summary>
        public void PopMementoStack()
        {
            var mementoArray = _mementoStack.Peek();
            for (int i = 0; i < _moveObjectEntity.Count(); i++)
            {
                _moveObjectEntity.Get(i).SetPosition(mementoArray[i].GetPosition());
            }

            _mementoStack.Pop();
        }

        public bool IsMementoStackEmpty() => _mementoStack.Count == 0;

        public bool IsMove() => _moveObjectEntity.moveObjects.All(moveObject => moveObject.isStop);
    }
}